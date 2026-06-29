using KiwiTracker.API.Data;
using KiwiTracker.API.DTOs;
using KiwiTracker.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace KiwiTracker.API.Services;

public class GoalService : IGoalService
{
    private readonly AppDbContext _context;
    private readonly IDistributedCache _cache;
    private readonly ILogger<GoalService> _logger;

    private static readonly DistributedCacheEntryOptions CacheOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
    };

    public GoalService(AppDbContext context, IDistributedCache cache, ILogger<GoalService> logger)
    {
        _context = context;
        _cache = cache;
        _logger = logger;
    }

    public async Task<GoalResponseDto> CreateGoalAsync(int userId, CreateGoalDto dto)
    {
        var goal = new Goal
        {
            Title = dto.Title,
            Description = dto.Description,
            UserId = userId,
            Status = GoalStatus.NotStarted,
            CreatedAt = DateTime.UtcNow
        };

        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();

        await InvalidateUserGoalsCache(userId);

        _logger.LogInformation("Goal created: {GoalId} for user {UserId}", goal.Id, userId);

        return MapToResponseDto(goal);
    }

    public async Task<List<GoalResponseDto>> GetUserGoalsAsync(int userId)
    {
        var cacheKey = GetUserGoalsCacheKey(userId);
        var cached = await _cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cached))
        {
            _logger.LogDebug("Cache hit for user goals: {UserId}", userId);
            return JsonSerializer.Deserialize<List<GoalResponseDto>>(cached) ?? [];
        }

        var goals = await _context.Goals
            .Where(g => g.UserId == userId)
            .OrderByDescending(g => g.CreatedAt)
            .ToListAsync();

        var result = goals.Select(MapToResponseDto).ToList();

        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(result), CacheOptions);
        _logger.LogDebug("Cache miss for user goals: {UserId}, cached {Count} goals", userId, result.Count);

        return result;
    }

    public async Task<GoalResponseDto?> GetGoalByIdAsync(int userId, int goalId)
    {
        var cacheKey = GetGoalCacheKey(userId, goalId);
        var cached = await _cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cached))
        {
            _logger.LogDebug("Cache hit for goal: {GoalId}", goalId);
            return JsonSerializer.Deserialize<GoalResponseDto>(cached);
        }

        var goal = await _context.Goals
            .FirstOrDefaultAsync(g => g.Id == goalId && g.UserId == userId);

        if (goal == null)
            return null;

        var responseDto = MapToResponseDto(goal);
        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(responseDto), CacheOptions);

        return responseDto;
    }

    public async Task<GoalResponseDto?> UpdateGoalAsync(int userId, int goalId, UpdateGoalDto dto)
    {
        var goal = await _context.Goals
            .FirstOrDefaultAsync(g => g.Id == goalId && g.UserId == userId);

        if (goal == null)
            return null;

        if (dto.Title != null)
            goal.Title = dto.Title;

        if (dto.Description != null)
            goal.Description = dto.Description;

        if (dto.Status.HasValue)
        {
            goal.Status = dto.Status.Value;

            if (dto.Status.Value == GoalStatus.Completed && goal.CompletedAt == null)
                goal.CompletedAt = DateTime.UtcNow;
        }

        if (dto.CompletedAt.HasValue)
            goal.CompletedAt = dto.CompletedAt.Value;

        await _context.SaveChangesAsync();

        await InvalidateGoalCache(userId, goalId);
        await InvalidateUserGoalsCache(userId);

        _logger.LogInformation("Goal updated: {GoalId} for user {UserId}", goalId, userId);

        return MapToResponseDto(goal);
    }

    public async Task<bool> DeleteGoalAsync(int userId, int goalId)
    {
        var goal = await _context.Goals
            .FirstOrDefaultAsync(g => g.Id == goalId && g.UserId == userId);

        if (goal == null)
            return false;

        _context.Goals.Remove(goal);
        await _context.SaveChangesAsync();

        await InvalidateGoalCache(userId, goalId);
        await InvalidateUserGoalsCache(userId);

        _logger.LogInformation("Goal deleted: {GoalId} for user {UserId}", goalId, userId);

        return true;
    }

    // -----------------------------------------------------------------------
    // Cache helpers
    // -----------------------------------------------------------------------

    private static string GetGoalCacheKey(int userId, int goalId) => $"goal:{userId}:{goalId}";
    private static string GetUserGoalsCacheKey(int userId) => $"user-goals:{userId}";

    private async Task InvalidateGoalCache(int userId, int goalId)
    {
        await _cache.RemoveAsync(GetGoalCacheKey(userId, goalId));
    }

    private async Task InvalidateUserGoalsCache(int userId)
    {
        await _cache.RemoveAsync(GetUserGoalsCacheKey(userId));
    }

    // -----------------------------------------------------------------------
    // Mapping
    // -----------------------------------------------------------------------

    private static GoalResponseDto MapToResponseDto(Goal goal)
    {
        return new GoalResponseDto
        {
            Id = goal.Id,
            Title = goal.Title,
            Description = goal.Description,
            Status = goal.Status,
            CreatedAt = goal.CreatedAt,
            CompletedAt = goal.CompletedAt,
            UserId = goal.UserId
        };
    }
}
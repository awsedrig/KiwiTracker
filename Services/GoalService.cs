using KiwiTracker.API.Data;
using KiwiTracker.API.DTOs;
using KiwiTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace KiwiTracker.API.Services;

public class GoalService : IGoalService
{
    private readonly AppDbContext _context;

    public GoalService(AppDbContext context)
    {
        _context = context;
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

        return MapToResponseDto(goal);
    }

    public async Task<List<GoalResponseDto>> GetUserGoalsAsync(int userId)
    {
        var goals = await _context.Goals
        .Where(g => g.UserId == userId)
        .OrderByDescending(g => g.CreatedAt)
        .ToListAsync();

        return goals.Select(MapToResponseDto).ToList();
    }

    public async Task<GoalResponseDto?> GetGoalByIdAsync(int userId, int goalId)
    {
        var goal = await _context.Goals
        .FirstOrDefaultAsync(g => g.Id == goalId && g.UserId == userId);
        return goal == null ? null : MapToResponseDto(goal);
    }

    public async Task<GoalResponseDto?> UpdateGoalAsync(int userId, int goalId, UpdateGoalDto dto)
    {
        // Find goal and verify ownership
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

            // Auto-set CompletedAt when status changes to Completed
            if (dto.Status.Value == GoalStatus.Completed && goal.CompletedAt == null)
                goal.CompletedAt = DateTime.UtcNow;
        }

        if (dto.CompletedAt.HasValue)
            goal.CompletedAt = dto.CompletedAt.Value;

        await _context.SaveChangesAsync();

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

        return true;
    }

    private GoalResponseDto MapToResponseDto(Goal goal)
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
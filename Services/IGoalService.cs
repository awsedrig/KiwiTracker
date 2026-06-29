using KiwiTracker.API.DTOs;

namespace KiwiTracker.API.Services;

public interface IGoalService
{
    Task<GoalResponseDto> CreateGoalAsync(int userId, CreateGoalDto dto);
    
    Task<List<GoalResponseDto>> GetUserGoalsAsync(int userId);
    
    Task<GoalResponseDto?> GetGoalByIdAsync(int userId, int goalId);
    
    Task<GoalResponseDto?> UpdateGoalAsync(int userId, int goalId, UpdateGoalDto dto);
    
    Task<bool> DeleteGoalAsync(int userId, int goalId);
}

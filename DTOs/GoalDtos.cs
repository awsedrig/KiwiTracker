using KiwiTracker.API.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace KiwiTracker.API.DTOs;

public class CreateGoalDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class UpdateGoalDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public GoalStatus? Status { get; set; }
    public DateTime? CompletedAt { get; set; }
}

public class GoalResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public GoalStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int UserId { get; set; }
}

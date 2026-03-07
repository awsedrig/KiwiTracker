using KiwiTracker.API.Models;
using System.ComponentModel.DataAnnotations;

namespace KiwiTracker.API.DTOs;

public class CreateGoalDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(500)]
    public string? Description { get; set; }
}

public class UpdateGoalDto
{
    [StringLength(100, MinimumLength = 3)]
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

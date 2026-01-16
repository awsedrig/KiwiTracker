using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace KiwiTracker.API.Models;

public enum GoalStatus
    {
        NotStarted = 0,
        InProgress = 1,
        Completed = 2,
        Abandoned = 3
    }

public class Goal
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public GoalStatus Status { get; set; } = GoalStatus.NotStarted;
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
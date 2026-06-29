using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KiwiTracker.API.DTOs;
using KiwiTracker.API.Extensions;
using KiwiTracker.API.Services;

namespace KiwiTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GoalsController : ControllerBase
{
    private readonly IGoalService _goalService;

    public GoalsController(IGoalService goalService)
    {
        _goalService = goalService;
    }

    /// <summary>
    /// Creates a new goal for the authenticated user.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(GoalResponseDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<GoalResponseDto>> CreateGoal([FromBody] CreateGoalDto dto)
    {
        var userId = User.GetUserId();
        var result = await _goalService.CreateGoalAsync(userId, dto);
        return CreatedAtAction(nameof(GetGoalById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Returns all goals for the authenticated user.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<GoalResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<GoalResponseDto>>> GetUserGoals()
    {
        var userId = User.GetUserId();
        var result = await _goalService.GetUserGoalsAsync(userId);
        return Ok(result);
    }

    /// <summary>
    /// Returns a specific goal by ID.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GoalResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GoalResponseDto>> GetGoalById(int id)
    {
        var userId = User.GetUserId();
        var result = await _goalService.GetGoalByIdAsync(userId, id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Updates an existing goal.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(GoalResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GoalResponseDto>> UpdateGoal(int id, [FromBody] UpdateGoalDto dto)
    {
        var userId = User.GetUserId();
        var result = await _goalService.UpdateGoalAsync(userId, id, dto);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Deletes a goal.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGoal(int id)
    {
        var userId = User.GetUserId();
        var success = await _goalService.DeleteGoalAsync(userId, id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}

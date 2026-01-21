using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using KiwiTracker.API.DTOs;
using KiwiTracker.API.Services;

namespace KiwiTracker.Controllers;

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

    [HttpPost]
    public async Task<ActionResult<GoalResponseDto>> CreateGoal([FromBody] CreateGoalDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _goalService.CreateGoalAsync(userId, dto);
        return CreatedAtAction(nameof(GetGoalById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<ActionResult<List<GoalResponseDto>>> GetUserGoals()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _goalService.GetUserGoalsAsync(userId);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GoalResponseDto>> GetGoalById(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _goalService.GetGoalByIdAsync(userId, id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GoalResponseDto>> UpdateGoal(int id,
    [FromBody] UpdateGoalDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _goalService.UpdateGoalAsync(userId, id, dto);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGoal(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var success = await _goalService.DeleteGoalAsync(userId, id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}

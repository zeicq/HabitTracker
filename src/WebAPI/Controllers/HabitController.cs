using Application.Features.Habits.Command.Create;
using Application.Features.Habits.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class HabitController : ControllerBase
{
    private readonly IMediator _mediator;

    public HabitController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateHabit([FromBody] CreateHabitCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok("Habit created successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create habit: {ex.Message}");
        }
    }

    [HttpGet("{habitId}")]
    public async Task<IActionResult> GetHabit(int habitId)
    {
        try
        {
            var query = new GetHabitQuery { Id = habitId };
            var habit = await _mediator.Send(query);

            if (habit == null)
            {
                return NotFound();
            }

            return Ok(habit);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to get habit: {ex.Message}");
        }
    }
}
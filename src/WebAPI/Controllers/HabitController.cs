using Application.Features.Habits.Command.Create;
using Application.Features.Habits.Command.Delete;
using Application.Features.Habits.Command.Update;
using Application.Features.Habits.Queries.All;
using Application.Features.Habits.Queries.Id;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class HabitController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;


    public HabitController(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateHabit([FromBody] CreateHabitCommand command)
    {
        try
        {
            var httpsUrl = _configuration.GetSection("PageUrl")["Https"];
            var create_habit = await _mediator.Send(command);
            var id = create_habit.Data.Id;


            var newHabitUrl = $"{httpsUrl}/Habit/{id}";
            return Created(newHabitUrl, $"{newHabitUrl}");
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create a habit: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetHabit(int id)
    {
        try
        {
            var query = new GetHabitQuery { Id = id };
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

    [HttpGet(Name = "GetAll")]
    public async Task<ActionResult<List<HabitViewModel>>> GetAllPosts()
    {
        var list = await _mediator.Send(new GetHabitsQuery());
        return Ok(list);
    }

    [HttpPut(Name = "UpdateHabit")]
    public async Task<ActionResult> Update([FromBody] UpdateHabitCommand updateCommand)
    {
        await _mediator.Send(updateCommand);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteHabit")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(int id)
    {
        var deletepostCommand = new DeleteHabitCommand() { Id = id };
        await _mediator.Send(deletepostCommand);
        return NoContent();
    }
}
using Application.Features.Habits.Command.Create;
using Application.Features.Habits.Command.Delete;
using Application.Features.Habits.Command.Update;
using Application.Features.Habits.Queries;
using Application.Features.Habits.Queries.All;
using Application.Features.Habits.Queries.Id;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class HabitController : CommonApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateHabit([FromBody] CreateHabitCommand command)
    {
        var response = await Mediator.Send(command);
        return Created(UrlResponse, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetHabitById(int id)
    {
        return Ok(await Mediator.Send(new GetHabitByIdQuery() { Id = id }));
    }

    [HttpGet(Name = "GetAll")]
    public async Task<ActionResult<List<HabitViewModel>>> GetAllPosts()
    {
        return Ok(await Mediator.Send(new GetHabitsQuery()));
    }

    [HttpPut(Name = "UpdateHabit")]
    public async Task<ActionResult> Update([FromBody] UpdateHabitCommand updateCommand)
    {
        await Mediator.Send(updateCommand);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteHabit")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = (await Mediator.Send(new DeleteHabitCommand() { Id = id }));
        return NoContent();
    }
}
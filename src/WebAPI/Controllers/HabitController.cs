using Application.Features.Habits.Command.Create;
using Application.Features.Habits.Command.Delete;
using Application.Features.Habits.Command.Update;
using Application.Features.Habits.Queries;
using Application.Features.Habits.Queries.All;
using Application.Features.Habits.Queries.Id;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers;
[Authorize]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ApiController]
[Route("[controller]")]
public class HabitController : CommonApiController
{
   
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateHabit([FromBody] CreateHabitCommand command)
    {
        var response = await Mediator.Send(command);
        return Created(UrlResponse, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHabitById(int id)
    {
        return Ok(await Mediator.Send(new GetHabitByIdQuery() { Id = id }));
    }

    [HttpGet(Name = "GetAll")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<HabitViewModel>>> GetAllPosts(int pageSize=1,int page=10)
    {
        return Ok(await Mediator.Send(new GetHabitsQuery(){PageSize = pageSize,PageNumber = page}));
    }

    [HttpPut(Name = "UpdateHabit")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
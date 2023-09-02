using Application.Features.Schedules.Command.Create;
using Application.Features.Schedules.Command.Delete;
using Application.Features.Schedules.Command.Update;
using Application.Features.Schedules.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleController : CommonApiController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleCommand command)
    {
        var response = await Mediator.Send(command);
        return Created(UrlResponse, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSchedule(int id)
    {
        return Ok(await Mediator.Send(new GetScheduleByIdQuery() { Id = id }));
    }
    
    [HttpPut(Name = "UpdateSchedule")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update([FromBody] UpdateScheduleCommand updateCommand)
    {
        await Mediator.Send(updateCommand);
        return NoContent();
    }
    
    [HttpDelete("{id}", Name = "DeleteSchedule")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = (await Mediator.Send(new DeleteScheduleCommand() { Id = id }));
        return NoContent();
    }
}
using Application.Features.Schedules.Command.Create;
using Application.Features.Schedules.Queries;
using FluentValidation;
using MediatR;
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
    public async Task<IActionResult> GetSchedule(int id)
    {
        return Ok(await Mediator.Send(id));
    }
}
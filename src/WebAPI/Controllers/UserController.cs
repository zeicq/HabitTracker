using Application.Features.Users.Command.GenerateToken;
using Application.Features.Users.Command.Register;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : CommonApiController
{
    
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var register = await Mediator.Send(command);
        return register.Succeeded
            ? Ok()
            : BadRequest(register.Errors);
    }

    [HttpPost("token")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetToken(GenerateTokenCommand command)
    {
        var response = await Mediator.Send(command);
        return response.Succeeded
            ? Ok(new { token = response.Data })
            : Unauthorized("Invalid Credentials");
    }
}
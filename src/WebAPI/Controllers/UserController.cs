using Application.Features.Users.Command.Register;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : CommonApiController
{
    
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var register =await Mediator.Send(command);
        return register.Succeeded
            ? Ok()
            : BadRequest(register.Errors);

    }
}
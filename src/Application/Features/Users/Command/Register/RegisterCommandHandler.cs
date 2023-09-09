using System.Net;
using Application.Features.Users.Command.AddRole;
using Application.Services;
using Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Command.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<Unit>>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IMediator _mediator;

    public RegisterCommandHandler(UserManager<IdentityUser> userManager, IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
    }

    public async Task<Response<Unit>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var user = new IdentityUser { Email = command.Email, UserName = command.Email };
        var createUserResult = await _userManager.CreateAsync(user, command.Password);
        if (!createUserResult.Succeeded)
        {
            return new Response<Unit>(createUserResult.Errors.Select(e => e.Description).ToList());
        }

        var addRule = await _mediator.Send(new AddRoleCommand(user, command.Role));
        return new Response<Unit>(true);
    }
}
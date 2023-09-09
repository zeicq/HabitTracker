using Application.Enums;
using Application.Features.Users.Command.RegistrationMessage;
using Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Command.AddRole;

public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, Response<Unit>>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IMediator _mediator;

    public AddRoleCommandHandler(UserManager<IdentityUser> userManager, IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
    }

    public async Task<Response<Unit>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        switch (request.Role)
        {
            case Roles.Admin:
                await _userManager.AddToRoleAsync(request.IdentityUser, request.Role.ToString());
                break;
            case Roles.Manager:
                await _userManager.AddToRoleAsync(request.IdentityUser, request.Role.ToString());
                break;
            case Roles.User:
                await _userManager.AddToRoleAsync(request.IdentityUser, request.Role.ToString());
                break;
            default:
                throw new ArgumentException("no recognized role");
        }

        await _mediator.Send(new RegistrationMessageCommand(request.IdentityUser));
        return new Response<Unit>(true);
    }
}
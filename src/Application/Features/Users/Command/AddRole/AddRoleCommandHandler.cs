using Application.Features.Users.Command.RegistrationMessage;
using Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Command.AddRole;

public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, Response<Unit>>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IMediator _mediator;

    public AddRoleCommandHandler(UserManager<IdentityUser> userManager,IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
    }

    public async Task<Response<Unit>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        var addRoleResult = await _userManager.AddToRoleAsync(request.IdentityUser, request.Role);
        if (!addRoleResult.Succeeded)
        {
            return new Response<Unit>(addRoleResult.Errors.Select(e => e.Description).ToList());
        }

        await _mediator.Send(new RegistrationMessageCommand(request.IdentityUser, request.Role));
        return new Response<Unit>(true);
    }
}
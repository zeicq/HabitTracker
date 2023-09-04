using Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Command.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<Unit>>
{
    private readonly UserManager<IdentityUser> _userManager;

    public RegisterCommandHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Response<Unit>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var user = new IdentityUser { Email = command.Email, UserName = command.Email };

        var createUserResult = await _userManager.CreateAsync(user, command.Password);
        if (!createUserResult.Succeeded)
        {
            return new Response<Unit>(createUserResult.Errors.Select(e => e.Description).ToList());
        }

        var addRoleResult = await _userManager.AddToRoleAsync(user, "User");

        if (!addRoleResult.Succeeded)
        {
            return new Response<Unit>(addRoleResult.Errors.Select(e => e.Description).ToList());
        }

        return new Response<Unit>(true);
    }

}
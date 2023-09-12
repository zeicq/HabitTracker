using Application.Features.Users.Command.AddRole;
using Application.Features.UsersProfile;
using Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.Users.Command.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<Unit>>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IMediator _mediator;
    private readonly ILogger<RegisterCommandHandler> _logger;
    

    public RegisterCommandHandler(UserManager<IdentityUser> userManager, IMediator mediator, ILogger<RegisterCommandHandler> loger)
    {
        _userManager = userManager;
        _mediator = mediator;
        _logger = loger;
    }

    public async Task<Response<Unit>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var user = new IdentityUser { Email = command.Email, UserName = command.Email };
        var createUserResult = await _userManager.CreateAsync(user, command.Password);
        if (!createUserResult.Succeeded)
        {
            _logger.LogError("RegisterCommandHandler - problem with register new user: " +
                          createUserResult.Errors.Select(x=>x.Description));
            return new Response<Unit>(createUserResult.Errors.Select(e => e.Description).ToList());
        }

        var addRule = await _mediator.Send(new AddRoleCommand(user, command.Role));

        var newUser = await _userManager.FindByEmailAsync(command.Email);
        var addUserProfile = await _mediator.Send(new CreateUserProfileCommand() { IdUser = newUser.Id });

        return new Response<Unit>(true);
    }
}
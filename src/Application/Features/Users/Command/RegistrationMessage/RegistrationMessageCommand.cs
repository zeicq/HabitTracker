using Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Command.RegistrationMessage;

public class RegistrationMessageCommand : IRequest<Response<Unit>>
{
    public RegistrationMessageCommand(IdentityUser identityUser)
    {
        IdentityUser = identityUser;
    }

    public IdentityUser IdentityUser { get; set; }
}
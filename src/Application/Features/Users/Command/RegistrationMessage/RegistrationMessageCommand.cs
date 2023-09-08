using Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Command.RegistrationMessage;

public class RegistrationMessageCommand : IRequest<Response<Unit>>
{
    public RegistrationMessageCommand(IdentityUser identityUser, string role)
    {
        IdentityUser = identityUser;
        Role = role;
    }

    public IdentityUser IdentityUser { get; set; }
    public string Role { get; set; }
}
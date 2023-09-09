using Application.Enums;
using Application.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Command.AddRole;

public class AddRoleCommand : IRequest<Response<Unit>>
{
    public AddRoleCommand(IdentityUser user, Roles role)
    {
        IdentityUser = user;
        Role = role;
    }

    public IdentityUser IdentityUser { get; set; }
    public Roles Role { get; set; }
}
using Application.Enums;
using Application.Shared;
using MediatR;

namespace Application.Features.Users.Command.Register;

public class RegisterCommand : IRequest<Response<Unit>>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public Roles Role { get; set; }
}
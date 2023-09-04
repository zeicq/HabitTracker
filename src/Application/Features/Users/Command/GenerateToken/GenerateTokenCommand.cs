using Application.Shared;
using MediatR;

namespace Application.Features.Users.Command.GenerateToken;

public class GenerateTokenCommand  : IRequest<Response<string>>
{
    public string Email { get; set; }
    public string Password { get; set; }

}
using Application.Shared;
using MediatR;

namespace Application.Features.UsersProfile;

public class CreateUserProfileCommand : IRequest<Response<Unit>>
{
    public string IdUser { get; set; }
}
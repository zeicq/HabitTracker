using Application.Shared;
using Domain.Entity;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.UsersProfile;

public class CreateUserProfileCommandHandler: IRequestHandler<CreateUserProfileCommand, Response<Unit>>
{
    private readonly IUserProfileRepository _repository;
    public CreateUserProfileCommandHandler(IUserProfileRepository repository)
    {
        _repository = repository;
    }
    public async Task<Response<Unit>> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var profile = new UserProfile() { UserId = request.IdUser };
        try
        {
            await _repository.AddAsync(profile);
            return new Response<Unit>(true);
        }
        catch (Exception ex)
        {
            return new Response<Unit>($"Error when saving a user profile: {ex.Message}");
        }
    }
}
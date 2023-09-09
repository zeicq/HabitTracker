using Domain.Entity;

namespace Domain.Interfaces;

public interface IUserProfileRepository
{
    Task AddAsync(UserProfile user);
    Task UpdateAsync(UserProfile user);
}
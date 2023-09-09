using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserProfileRepository
{
    private readonly MssqlDbContext _dbContext;

    public UserRepository(MssqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(UserProfile user)
    {
        await _dbContext.UsersProfile.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserProfile user)
    {
        _dbContext.Entry(user).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}
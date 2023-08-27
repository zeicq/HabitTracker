using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProgressRepository : IProgressRepository
{
    private readonly MssqlDbContext _dbContext;

    public ProgressRepository(MssqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Progress> AddAsync(Progress progress)
    {
        return await _dbContext.Set<Progress>().FindAsync(progress);
    }

    public async Task UpdateAsync(Progress progress)
    {
        _dbContext.Entry(progress).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}
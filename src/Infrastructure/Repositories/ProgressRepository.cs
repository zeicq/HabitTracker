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

    public async Task<Progress> GetByIdAsync(int id)
    {
          return await _dbContext.Progresses.FindAsync(id);
    }

    public async Task<Progress> AddAsync(Progress progress)
    {
        await _dbContext.AddAsync(progress);
        await _dbContext.SaveChangesAsync();
        return progress;
    }

    public async Task UpdateAsync(Progress progress)
    {
        _dbContext.Entry(progress).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}
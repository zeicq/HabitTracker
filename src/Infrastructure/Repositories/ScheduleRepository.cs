using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ScheduleRepository : GenericRepositoryBaseAsync<Schedule>, IScheduleRepository
{
    private readonly DbSet<Schedule> _dbContext;


    public ScheduleRepository(MssqlDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext.Set<Schedule>();
    }


    public async Task<bool> IsUniqueScheduleAsync(int habitId)
    {
        return !await _dbContext.AnyAsync(s => s.HabitId == habitId);
    }
}
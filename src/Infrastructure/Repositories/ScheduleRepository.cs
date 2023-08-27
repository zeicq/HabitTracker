using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ScheduleRepository : GenericRepositoryBaseAsync<Schedule>, IScheduleRepository
{
    private readonly MssqlDbContext _dbContext;

    public ScheduleRepository(MssqlDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Schedule>> GetScheduledBetweenDates(DateTime startDate, DateTime endDate)
    {
        return await _dbContext.Schedules
            .Where(schedule => schedule.TimeOfDay >= startDate.TimeOfDay && schedule.TimeOfDay <= endDate.TimeOfDay)
            .ToListAsync();
    }
}
using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ScheduleEntryRepository : GenericRepositoryBaseAsync<ScheduleEntry>, IScheduleEntryRepository
{
    private readonly DbSet<ScheduleEntry> _dbContext;

    public ScheduleEntryRepository(MssqlDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext.Set<ScheduleEntry>();
    }
}
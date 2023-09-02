using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class HabitRepository : GenericRepositoryBaseAsync<Habit>, IHabitRepository
{
    private readonly DbSet<Habit> _dbContext;


    public HabitRepository(MssqlDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext.Set<Habit>();
    }
    
    public async Task<bool> IsUniqueHabitAsync(string name)
    {
        return !await _dbContext.AnyAsync(h => h.Name == name);
    }
}
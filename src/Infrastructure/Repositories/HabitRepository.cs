using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class HabitRepository : IHabitRepository
{
    private readonly MssqlDbContext _dbContext;

    public HabitRepository(MssqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Habit> GetByIdAsync(int id)
    {
        return await _dbContext.Habits.FindAsync(id);
    }

    public async Task<List<Habit>> GetAllAsync()
    {
        return await _dbContext.Habits.ToListAsync();
    }

    public async Task<Habit> AddAsync(Habit habit)
    {
        await _dbContext.AddAsync(habit);
        await _dbContext.SaveChangesAsync();
        return habit;
    }

    public async Task UpdateAsync(Habit habit)
    {
        _dbContext.Entry(habit).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Habit habit)
    {
        _dbContext.Remove(habit);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsUniqueHabitAsync(string name)
    {
        return !await _dbContext.Habits.AnyAsync(h => h.Name == name);
    }
}
using Domain.Entity;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class InMemoryHabitRepository : IHabitRepository
{
    private static readonly ISet<Habit> _habit = new HashSet<Habit>();

    public async Task<List<Habit>> GetAllAsync()
    {
        return _habit.ToList();
    }

    public async Task<Habit> GetByIdAsync(int id)
    {
        return _habit.SingleOrDefault(x => x.Id == id);
    }

    public async Task AddAsync(Habit habit)
    {
        _habit.Add(habit);
    }

    public async Task UpdateAsync(Habit habit)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Habit habit)
    {
        throw new NotImplementedException();
    }
}
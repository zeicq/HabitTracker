using Domain.Entity;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class InMemoryHabitRepository : IHabitRepository
{
    private static readonly ISet<Habit> _habit = new HashSet<Habit>();

    public async Task<List<Habit>> GetAllAsync()
    {
        return await Task.Run(() => _habit.ToList());
    }

    public async Task<Habit> GetByIdAsync(int id)
    {
        return await Task.Run(() => _habit.SingleOrDefault(x => x.Id == id));
    }
    
    public async Task AddAsync(Habit habit)
    {
        await Task.Run(() => _habit.Add(habit));
    }

    public async Task UpdateAsync(Habit habit)
    {
        await Task.Run(() =>
            {
                Habit existingHabit = _habit.FirstOrDefault(h => h.Id == habit.Id);

                if (existingHabit != null)
                {
                    existingHabit.Name = habit.Name;
                    existingHabit.Description = habit.Description;
                }
                else
                {
                    throw new KeyNotFoundException("Habit not found.");
                }
            });
    }


    public async Task DeleteAsync(Habit habit)
    {
        await Task.Run(() =>
            {
                Habit existingHabit = _habit.FirstOrDefault(h => h.Id == habit.Id);

                if (existingHabit != null)
                {
                    _habit.Remove(existingHabit);
                }
                else
                {
                    throw new KeyNotFoundException("Habit not found.");
                }
            });
    }

}
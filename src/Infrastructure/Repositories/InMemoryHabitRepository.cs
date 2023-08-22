using Domain.Entity;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class InMemoryHabitRepository : IHabitRepository
{
    private static readonly ISet<Habit> _habit = new HashSet<Habit>()
    {
        new Habit(){Name = "Learn .NET",Description = "Nothig",IsCompleted = true},
        new Habit(){Name = "Learn English",Description = "Nothig",IsCompleted = true,},
    };
    
    

    public async Task<List<Habit>> GetAllAsync()
    {
        var tasks = await Task.FromResult(_habit.ToList());
        return tasks;
    }

    public async Task<Habit> GetByIdAsync(int id)
    {
        var habit = await Task.FromResult(_habit.SingleOrDefault(x => x.Id == id));
        return habit;
    }

    public async Task AddAsync(Habit habit)
    {
        await Task.Run(() => _habit.Add(habit));
    }

    public async Task UpdateAsync(Habit habit)
    {
        await Task.Run(() => 
        {
            var existingHabit = _habit.FirstOrDefault(h => h.Id == habit.Id);
            if (existingHabit != null)
            {
                existingHabit.Name = habit.Name;
                existingHabit.Description = habit.Description;
            }
            else
            {
                throw new ArgumentException("Habit not found");
            }
        });
    }

    public Task DeleteAsync(Habit habit)
    {
        throw new NotImplementedException();
    }
}
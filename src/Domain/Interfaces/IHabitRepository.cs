using Domain.Entity;

namespace Domain.Interfaces;

public interface IHabitRepository
{
    Task<Habit> GetByIdAsync(int id);
    Task<List<Habit>> GetAllAsync();
    Task<Habit> AddAsync(Habit habit);
    Task UpdateAsync(Habit habit);
    Task DeleteAsync(Habit habit);
    Task<bool> IsUniqueHabitAsync(string name);
}
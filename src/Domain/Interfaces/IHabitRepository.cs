using Domain.Entity;

namespace Domain.Interfaces;

public interface IHabitRepository
{
    Task<Habit> GetByIdAsync(int id);
    Task<List<Habit>> GetAllAsync();
    Task AddAsync(Habit habit);
    Task UpdateAsync(Habit habit);
    Task DeleteAsync(Habit habit);
}
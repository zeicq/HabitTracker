using Domain.Entity;

namespace Domain.Interfaces;

public interface IHabitRepository : IGenericRepositoryBaseAsync<Habit>
{
    Task<bool> IsUniqueHabitAsync(string name);
}
using Domain.Entity;

namespace Domain.Interfaces;

public interface IScheduleRepository: IGenericRepositoryBaseAsync<Schedule>
{
    Task<bool> IsUniqueScheduleAsync(int habitId);
   
}
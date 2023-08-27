using Domain.Entity;

namespace Domain.Interfaces;

public interface IScheduleRepository: IGenericRepositoryBaseAsync<Schedule>
{
    Task<IReadOnlyList<Schedule>> GetScheduledBetweenDates(DateTime startDate, DateTime endDate);
   
}
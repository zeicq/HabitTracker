using Domain.Enums;

namespace Application.Features.Schedules.Queries;

public class ScheduleViewModel
{
    public ICollection<DaysOfWeekEnum> DaysOfWeek { get; set; }
    public TimeSpan TimeOfDay { get; set; }
    public int HabitId { get; set; }
}
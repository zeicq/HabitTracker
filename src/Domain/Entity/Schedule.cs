using Domain.Base;
using Domain.Enums;

namespace Domain.Entity;

public class Schedule : BaseEntity
{
    public int Id { get; set; }
    public int HabitId { get; set; }
    public List<DayOfWeekEnum> DaysOfWeek { get; set; }
    public TimeSpan TimeOfDay { get; set; }
}
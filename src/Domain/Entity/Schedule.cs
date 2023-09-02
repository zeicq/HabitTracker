using Domain.Base;
using Domain.Enums;

namespace Domain.Entity;

public class Schedule : BaseEntity
{
    public ICollection<DaysOfWeekEnum> DaysOfWeek { get; set; }
    public TimeSpan TimeOfDay { get; set; }
    public DateTime StartData { get; set; }
    public ICollection<ScheduleEntry> Entries { get; set; }
    public int HabitId { get; set; } 
    public Habit Habit { get; set; }
   
}
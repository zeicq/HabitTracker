using Domain.Base;
using Domain.Enums;

namespace Domain.Entity;

public class Schedule : BaseEntity
{
    public ICollection<DaysOfWeekEnum> DaysOfWeek { get; set; }
    public TimeSpan TimeOfDay { get; set; }
    //TODO
   // public DateTime StartHabit { get; set; }
    //TODO
  //  public DateTime EndHabit { get; set; }
    //TODO
 //   public IEnumerable<DateTime> Reminders { get; set; } 
    public int HabitId { get; set; }
    public Habit Habit { get; set; }
}
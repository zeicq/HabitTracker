using Domain.Base;

namespace Domain.Entity;

public class Progress : BaseEntity
{
    public int HabitId { get; set; }
    public int CurrentStreakCount { get; set; }
    public int LongestStreakCount { get; set; }
    public Habit Habit { get; set; }
}
using Domain.Base;

namespace Domain.Entity;

public class Team : BaseEntity
{
    public string Name { get; set; }
    public List<User> UserIds { get; set; }
    public List<Habit> HabitIds { get; set; }
}
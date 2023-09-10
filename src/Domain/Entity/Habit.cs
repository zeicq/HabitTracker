using Domain.Base;

namespace Domain.Entity;

public class Habit : EntityAuditData, IEntityId<int>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int UnitToPerform { get; set; }
    public int CurrentStreakCount { get; set; }
    public int LongestStreakCount { get; set; }
    public Schedule Schedule { get; set; }
    public string UserId { get; set; }
    public UserProfile UserProfile { get; set; }
}
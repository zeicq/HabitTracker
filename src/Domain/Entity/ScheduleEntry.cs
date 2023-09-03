using Domain.Base;

namespace Domain.Entity;

public class ScheduleEntry: IEntityId<int>
{
    public int Id { get; set; }
    public DateTime Date { get; set; } 
    public int Day { get; set; }
    public bool IsCompleted { get; set; } 
    public int ScheduleId { get; set; } 
    public Schedule Schedule { get; set; }
}
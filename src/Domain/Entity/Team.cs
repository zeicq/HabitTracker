using Domain.Base;

namespace Domain.Entity;

public class Team : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<int> UserIds { get; set; }
    public List<int> HabitIds { get; set; }
}
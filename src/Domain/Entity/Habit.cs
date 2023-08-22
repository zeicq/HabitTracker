using Domain.Base;

namespace Domain.Entity;

public class Habit : BaseEntity
{
    public Habit()
    {
    }

    public Habit(int id, string name)
    {
        Name = name;
        Id = id;
    }

    public string Name { get;  set; }
    public string Description { get;  set; }
    public bool IsCompleted { get;  set; }
}
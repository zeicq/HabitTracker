﻿using Domain.Base;

namespace Domain.Entity;

public class Habit : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}
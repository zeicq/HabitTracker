﻿using Domain.Entity;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class InMemoryHabitRepository : IHabitRepository
{
    private static readonly ISet<Habit> _habit = new HashSet<Habit>()
    {
        new Habit(1,"C#"),
        new Habit(2,"English"),
        new Habit(3,"JavaScript")
    };
    
    

    public async Task<List<Habit>> GetAllAsync()
    {
        var tasks = await Task.FromResult(_habit.ToList());
        return tasks;
    }

    public async Task<Habit> GetByIdAsync(int id)
    {
        var habit = await Task.FromResult(_habit.SingleOrDefault(x => x.Id == id));
        return habit;
    }

    public async Task AddAsync(Habit habit)
    {
        await Task.Run(() => _habit.Add(habit));
    }

    public async Task UpdateAsync(Habit habit)
    {
        await Task.Run(() => 
        {
            var existingHabit = _habit.FirstOrDefault(h => h.Id == habit.Id);
            if (existingHabit != null)
            {
                existingHabit.Name = habit.Name;
                existingHabit.Description = habit.Description;
                existingHabit.IsCompleted = habit.IsCompleted;
            }
            else
            {
                throw new ArgumentException("Habit not found");
            }
        });
    }


    public async Task DeleteAsync(Habit habit)
    {
        await Task.Run(() =>
            {
                Habit existingHabit = _habit.FirstOrDefault(h => h.Id == habit.Id);

                if (existingHabit != null)
                {
                    _habit.Remove(existingHabit);
                }
                else
                {
                    throw new KeyNotFoundException("Habit not found.");
                }
            });
    }
}
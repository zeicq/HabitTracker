﻿using Domain.Base;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class HabitConfiguration : IEntityTypeConfiguration<Habit>
{
    public void Configure(EntityTypeBuilder<Habit> builder)
    {
        builder.ToTable("Habits");
        builder.HasKey(se => se.Id);
        builder.Property(h => h.Name).IsRequired().HasMaxLength(100);
        builder.Property(h => h.Description).HasMaxLength(500);
        builder.Property(h =>h.UnitToPerform).IsRequired();        
        builder.Property(p => p.CurrentStreakCount).IsRequired();
        builder.Property(p => p.LongestStreakCount).IsRequired();
        builder.ConfigureEntityAuditData();
        
        builder.HasOne(h => h.UserProfile)
            .WithMany(u => u.Habits)
            .HasForeignKey(h => h.UserId)
            .IsRequired();
    }
}
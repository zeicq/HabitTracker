using Domain.Base;
using Domain.Entity;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable("Schedules");
        builder.ConfigureBaseEntity();

        builder.Property(s => s.StartData).IsRequired();
        builder.Property(s => s.TimeOfDay).IsRequired();
        builder.Property(s => s.DaysOfWeek)
            .HasConversion(
                v => string.Join(",", v),
                v => v.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => Enum.Parse<DaysOfWeekEnum>(s))
                    .ToList());
        
        builder.HasMany(s => s.Entries)
            .WithOne(e => e.Schedule)
            .HasForeignKey(e => e.ScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(s =>s.Habit)
            .WithOne(s => s.Schedule)
            .HasForeignKey<Schedule>(s => s.HabitId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
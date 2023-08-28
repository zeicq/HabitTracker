using Domain.Base;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ProgressConfiguration : IEntityTypeConfiguration<Progress>
{
    public void Configure(EntityTypeBuilder<Progress> builder)
    {
        builder.ToTable("Progress");
        builder.ConfigureBaseEntity();

        builder.Property(p => p.CurrentStreakCount).IsRequired();
        builder.Property(p => p.LongestStreakCount).IsRequired();
        
        builder.HasOne(p => p.Habit)
            .WithOne(h => h.Progress)
            .HasForeignKey<Progress>(p => p.HabitId) 
            .OnDelete(DeleteBehavior.Cascade);
    }
}
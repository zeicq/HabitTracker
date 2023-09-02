using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ScheduleEntryConfiguration: IEntityTypeConfiguration<ScheduleEntry>
{
    public void Configure(EntityTypeBuilder<ScheduleEntry> builder)
    {
        builder.HasKey(se => se.Id);
        builder.Property(se => se.Date)
            .IsRequired();
        builder.Property(se => se.Day)
            .IsRequired();
        builder.Property(se => se.IsCompleted)
            .IsRequired();
    }
}
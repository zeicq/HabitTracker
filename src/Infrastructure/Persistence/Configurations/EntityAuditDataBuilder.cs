using Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public static class EntityAuditDataBuilder
{
    public static void ConfigureEntityAuditData<T>(this EntityTypeBuilder<T> builder) where T : EntityAuditData
    {
        builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy").HasMaxLength(100);
        builder.Property(e => e.Created).HasColumnName("Created");
        builder.Property(e => e.LastModifiedBy).HasColumnName("LastModifiedBy").HasMaxLength(100);
        builder.Property(e => e.LastModified).HasColumnName("LastModified");
    }
}
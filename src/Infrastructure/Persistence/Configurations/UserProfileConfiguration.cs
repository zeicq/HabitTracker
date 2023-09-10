using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserProfileConfiguration: IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    { 
        builder.HasKey(se => se.UserId);
        builder.Property(u=>u.FirstName).HasMaxLength(100).IsRequired(false);;
        builder.Property(u=>u.LastName).HasMaxLength(500).IsRequired(false);;
        builder.ConfigureEntityAuditData();

        builder.HasOne(u => u.IdentityUser)
            .WithOne()        
            .HasForeignKey<UserProfile>(u => u.UserId);

    }
}
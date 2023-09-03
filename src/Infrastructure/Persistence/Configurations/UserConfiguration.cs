using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    { 
        builder.Property(u=>u.FirstName).HasMaxLength(100);
        builder.Property(u=>u.LastName).HasMaxLength(500);
        builder.ConfigureEntityAuditData();
        
        builder.HasOne(u => u.IdentityUser) 
            .WithOne() 
            .HasForeignKey<User>(u => u.UserId); 
        
    }
}
using Domain.Base;
using Domain.Entity;
using Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public class MssqlDbContext : DbContext
{
    public MssqlDbContext(DbContextOptions<MssqlDbContext> options)
        : base(options)
    {
    }

    public DbSet<Habit> Habits { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<ScheduleEntry> ScheduleEntries { get; set; }
    public DbSet<UserProfile> UsersProfile { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUser>().ToTable("UsersIdentity");
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole {Id = "1",Name = "Admin",NormalizedName = "ADMIN"},
            new IdentityRole {Id = "2", Name = "User",NormalizedName = "USER"},
            new IdentityRole {Id = "3", Name = "Manager",NormalizedName = "MANAGER"});
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("IdentityUserRoles")
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.ApplyConfiguration(new HabitConfiguration());
        modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
        modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
      
        IHttpContextAccessor _contextAccessor= new HttpContextAccessor();
        var identityName = _contextAccessor.HttpContext?.User.Identity.Name;
        foreach (var entry in ChangeTracker.Entries<EntityAuditData>())
        {
   

            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = DateTime.UtcNow;
                    entry.Entity.CreatedBy = "registration";
                    entry.Entity.LastModified = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = "registration";
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModified = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = identityName;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
using Domain.Base;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Contexts;

public class MssqlDbContext : DbContext
{
    public MssqlDbContext(DbContextOptions<MssqlDbContext> options)
        : base(options)
    {
    }

    public DbSet<Habit> Habits { get; set; }
    public DbSet<Progress> Progresses { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Team> Teams { get; set; }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = DateTime.UtcNow;
                    entry.Entity.CreatedBy = "User";
                    entry.Entity.LastModified = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = "User";
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModified = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = "User";
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
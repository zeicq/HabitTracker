using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceCollection
{
    public static IServiceCollection InfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<MssqlDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("AzureConnection"),
                b => b.MigrationsAssembly(typeof(MssqlDbContext).Assembly.FullName)));
        services.AddTransient(typeof(IGenericRepositoryBaseAsync<>), typeof(GenericRepositoryBaseAsync<>));
        services.AddTransient<IHabitRepository, HabitRepository>();
        services.AddTransient<IScheduleRepository, ScheduleRepository>();
        services.AddTransient<IScheduleEntryRepository, ScheduleEntryRepository>();
        return services;
    }
}
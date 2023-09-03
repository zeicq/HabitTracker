using Domain.Interfaces;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceCollection
{
    public static IServiceCollection InfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var identityOptions = configuration.GetSection("IdentityOptions");
       
        services.AddDbContext<MssqlDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("AzureConnection"),
                b => b.MigrationsAssembly(typeof(MssqlDbContext).Assembly.FullName)));
        services.AddTransient(typeof(IGenericRepositoryBaseAsync<>), typeof(GenericRepositoryBaseAsync<>));

        services.AddIdentity<IdentityUser, IdentityRole>(
                options =>
                {
                    options.Password.RequireDigit = identityOptions.GetValue<bool>("Password:RequireDigit");
                    options.Password.RequireLowercase = identityOptions.GetValue<bool>("Password:RequireLowercase");
                    options.Password.RequireUppercase = identityOptions.GetValue<bool>("Password:RequireUppercase");
                    options.Password.RequireNonAlphanumeric = identityOptions.GetValue<bool>("Password:RequireNonAlphanumeric");
                    options.Password.RequiredLength = identityOptions.GetValue<int>("Password:RequiredLength");
                    options.Password.RequiredUniqueChars = identityOptions.GetValue<int>("Password:RequiredUniqueChars");
            
                    options.User.RequireUniqueEmail = identityOptions.GetValue<bool>("User:RequireUniqueEmail");
                    options.SignIn.RequireConfirmedEmail = identityOptions.GetValue<bool>("SignIn:RequireConfirmedEmail");
                })
            .AddEntityFrameworkStores<MssqlDbContext>()
            .AddDefaultTokenProviders();



        services.AddTransient<IHabitRepository, HabitRepository>();
        services.AddTransient<IScheduleRepository, ScheduleRepository>();
        services.AddTransient<IScheduleEntryRepository, ScheduleEntryRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        return services;
    }
}
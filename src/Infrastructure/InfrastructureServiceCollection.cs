using Application.Features.Users.Command.GenerateToken;
using Application.Services;
using Application.Shared;
using Domain.Interfaces;
using Domain.Settings;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Infrastructure.Services;
using MediatR;


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
        services.AddTransient<IUserProfileRepository, UserRepository>();
        
        services.AddHangfire(conf => conf
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("AzureConnection")));
        services.AddHangfireServer();
        
       
        services.AddTransient<IEmailService, EmailService>();
        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
        services.AddTransient<IRequestHandler<GenerateTokenCommand, Response<string>>, GenerateTokenCommandHandler>();
        return services;
    }
}
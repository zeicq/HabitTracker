using System.Reflection;
using Domain.Interfaces;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceCollection
{
    public static IServiceCollection InfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IHabitRepository, InMemoryHabitRepository>();
        return services;
    }
}
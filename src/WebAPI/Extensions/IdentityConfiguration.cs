using Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Extensions;

public static class IdentityConfigurationExtensions
{
    public static void ConfigureIdentityOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var identityOptions = configuration.GetSection("IdentityOptions");
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
    }
}

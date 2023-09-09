using Application.Enums;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Seeds;

public static class StandardUsers
{
    public static async Task SeedStandardAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var defaultPassword = "Test123!@#";

        await SeedUserAsync(userManager, "user@user.com", defaultPassword, Roles.User.ToString());
        await SeedUserAsync(userManager, "admin@admin.com", defaultPassword, Roles.Admin.ToString());
    }

    private static async Task SeedUserAsync(UserManager<IdentityUser> userManager, string email, string password,
        string role)
    {
        var existingUser = await userManager.FindByEmailAsync(email);

        if (existingUser == null)
        {
            var defaultUser = new IdentityUser
            {
                Email = email,
                UserName = email,
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(defaultUser, password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(defaultUser, role);
            }
        }
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebAppBackend.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string adminEmail = "admin@admin.com";
            string adminUserName = "Admin";
            string adminRole = "Admin";
            string userRole = "User";

            // Get password from secrets or environment
            string? adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD") ?? configuration["Passwords:Admin"];
         

            var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");


            if (string.IsNullOrWhiteSpace(adminPassword))
            {
                throw new InvalidOperationException("Admin password not configured.");
            }


            // Create roles if they don't exist
            foreach (var roleName in new[] { adminRole, userRole })
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create or update admin user
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var createResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (!createResult.Succeeded)
                    throw new Exception("Failed to create admin user: " +
                        string.Join(", ", createResult.Errors.Select(e => e.Description)));
            }
            else
            {
                // Optionally reset password if needed
                var token = await userManager.GeneratePasswordResetTokenAsync(adminUser);
                var resetResult = await userManager.ResetPasswordAsync(adminUser, token, adminPassword);
                if (!resetResult.Succeeded)
                    throw new Exception("Failed to reset admin password: " +
                        string.Join(", ", resetResult.Errors.Select(e => e.Description)));
            }

            // Add to Admin role if not already
            if (!await userManager.IsInRoleAsync(adminUser, adminRole))
            {
                await userManager.AddToRoleAsync(adminUser, adminRole);
            }

            // Ensure user is in Admin role
            if (!await userManager.IsInRoleAsync(adminUser, adminRole))
            {
                await userManager.AddToRoleAsync(adminUser, adminRole);
            }
        }
    }
}

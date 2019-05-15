using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Module5.Data
{
    public class SeedData
    {
        private const string AdminRole = "administrator";
        private const string AdminUserName = "admin@localhost.com";

        public static void Initialize(IServiceProvider serviceProvider)
        {
            var config = serviceProvider.GetRequiredService<IConfiguration>();
            var adminUserPassword = config["AdminUserPwd"];

            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                SeedRoles(serviceProvider);
                SeedUsers(serviceProvider, adminUserPassword);
            }
        }

        private static void SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            if (roleManager == null)
            {
                throw new Exception("roleManager is null");
            }

            if (!roleManager.RoleExistsAsync(AdminRole).Result)
            {
                roleManager.CreateAsync(new IdentityRole(AdminRole));
            }
        }

        private static void SeedUsers(IServiceProvider serviceProvider, string adminPwd)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = userManager.FindByEmailAsync(AdminUserName).Result;
            if (user == null)
            {
                user = new IdentityUser { UserName = AdminUserName, Email = AdminUserName };
                userManager.CreateAsync(user, adminPwd).Wait();
            }

            userManager.AddToRoleAsync(user, AdminRole).Wait();
        }
    }
}

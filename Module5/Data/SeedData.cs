using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Module5.Data
{
    public class SeedData
    {
        private const string AdminRole = "administrator";
        public static async Task Initialize(IServiceProvider serviceProvider, string adminUserName, string adminPwd)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                await CreateRole(serviceProvider, AdminRole);
                var adminID = await CreateAdmin(serviceProvider, adminPwd, adminUserName);
                await GrantAdminRoleToAdminUser(serviceProvider, adminID, AdminRole);
            }
        }

        private static async Task CreateRole(IServiceProvider serviceProvider, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            if (roleManager == null)
            {
                throw new Exception("roleManager is null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }


        private static async Task<string> CreateAdmin(IServiceProvider serviceProvider,
                                                        string adminPwd, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser { UserName = UserName };
                await userManager.CreateAsync(user, adminPwd);
            }

            return user.Id;
        }

        private static async Task<IdentityResult> GrantAdminRoleToAdminUser(IServiceProvider serviceProvider, string uid, string role)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var user = await userManager.FindByIdAsync(uid);
            if (user == null)
            {
                throw new Exception("User not exists!");
            }

            return await userManager.AddToRoleAsync(user, role);
        }
    }
}

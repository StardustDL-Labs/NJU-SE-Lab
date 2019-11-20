using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IACG.Data
{
    public static class SeedData
    {
        public static async Task InitializeDb(IServiceProvider services)
        {
            ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();
        }

        public static async Task InitializeIdentity(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            foreach (var name in Enum.GetNames(typeof(UserRoles)))
            {
                if (!await roleManager.RoleExistsAsync(name))
                {
                    await roleManager.CreateAsync(new IdentityRole(name));
                }
            }
            if (await userManager.FindByNameAsync("admin") == null)
            {
                var user = new ApplicationUser()
                {
                    Email = "admin@iacg.internal",
                    UserName = "admin",
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(user, "123456");
                await userManager.AddToRoleAsync(user, nameof(UserRoles.Administrator));
            }
        }


    }
}

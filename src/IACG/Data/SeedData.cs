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
        static Random Random { get; } = new Random();

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
                    Name = "管理员",
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(user, "123456");
                await userManager.AddToRoleAsync(user, nameof(UserRoles.Administrator));
            }
            if (await userManager.FindByNameAsync("enttest") == null)
            {
                var user = new ApplicationUser()
                {
                    Email = "enttest@iacg.test",
                    UserName = "enttest",
                    Name = "测试企业A",
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(user, "123456");
                await userManager.AddToRoleAsync(user, nameof(UserRoles.Enterprise));
            }
            if (await userManager.FindByNameAsync("enttest2") == null)
            {
                var user = new ApplicationUser()
                {
                    Email = "enttest2@iacg.test",
                    UserName = "enttest2",
                    Name = "测试企业B",
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(user, "123456");
                await userManager.AddToRoleAsync(user, nameof(UserRoles.Enterprise));
            }
            if (await userManager.FindByNameAsync("protest") == null)
            {
                var user = new ApplicationUser()
                {
                    Email = "protest@iacg.test",
                    UserName = "protest",
                    Name = "测试专家A",
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(user, "123456");
                await userManager.AddToRoleAsync(user, nameof(UserRoles.Professional));
            }
            if (await userManager.FindByNameAsync("protest2") == null)
            {
                var user = new ApplicationUser()
                {
                    Email = "protest2@iacg.test",
                    UserName = "protest2",
                    Name = "测试专家B",
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(user, "123456");
                await userManager.AddToRoleAsync(user, nameof(UserRoles.Professional));
            }
        }

        public static async Task InitializeApps(ApplicationDbContext context)
        {
            {
                var ent = await context.Users.FirstAsync(u => u.UserName == "enttest");
                foreach (var i in Enumerable.Range(1, 2))
                {
                    context.Apps.Add(new App
                    {
                        Name = $"App {i}",
                        Description = $"A short description for app {i}",
                        UserId = ent.Id,
                        LastModifyTime = DateTimeOffset.Now,
                    });
                }
            }

            {
                var ent = await context.Users.FirstAsync(u => u.UserName == "enttest2");
                foreach (var i in Enumerable.Range(3, 2))
                {
                    context.Apps.Add(new App
                    {
                        Name = $"App {i}",
                        Description = $"A short description for app {i}",
                        UserId = ent.Id,
                        LastModifyTime = DateTimeOffset.Now,
                    });
                }
            }

            await context.SaveChangesAsync();
        }

        public static async Task InitializeReviews(ApplicationDbContext context)
        {
            {
                var ent = await context.Users.FirstAsync(u => u.UserName == "protest");
                foreach (var app in context.Apps)
                {
                    context.Reviews.Add(new Review
                    {
                        AppId = app.Id,
                        Result = (ReviewResult)Random.Next(0, 3),
                        UserId = ent.Id,
                        LastModifyTime = DateTimeOffset.Now,
                    });
                }
            }

            {
                var ent = await context.Users.FirstAsync(u => u.UserName == "protest2");
                foreach (var app in context.Apps)
                {
                    context.Reviews.Add(new Review
                    {
                        AppId = app.Id,
                        Result = (ReviewResult)Random.Next(0, 3),
                        UserId = ent.Id,
                        LastModifyTime = DateTimeOffset.Now,
                    });
                }
            }

            await context.SaveChangesAsync();
        }
    }
}

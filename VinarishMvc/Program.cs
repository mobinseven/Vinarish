using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using VinarishMvc.Data;
using VinarishMvc.Areas.Identity.Models;
using System;

namespace VinarishMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var services = host.Services.CreateScope())
            {
                var dbContext = services.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userMgr = services.ServiceProvider.GetRequiredService<UserManager<VinarishUser>>();
                var roleMgr = services.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

                dbContext.Database.Migrate();

                var adminRole = new IdentityRole<Guid>("Admin");

                if (!dbContext.Roles.Any())
                {
                    roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
                }

                if (!dbContext.Users.Any(u => u.UserName == "admin"))
                {
                    var adminUser = new VinarishUser
                    {
                        UserName = "admin@vinarish.com",
                        Email = "admin@vinarish.com"
                    };
                    var result = userMgr.CreateAsync(adminUser, "Hibernate70!").GetAwaiter().GetResult();
                    userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dispatch_system.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dispatch_system
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var services = host.Services.CreateScope())
            {
                var dbContext = services.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userMgr = services.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleMgr = services.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                dbContext.Database.Migrate();

                var managerRole = new IdentityRole("Kierownik");
                var courierRole = new IdentityRole("Kurier");
                var branchEmployeeRole = new IdentityRole("Pracownik oddziału");
                var clientRole = new IdentityRole("Klient");
                var warehousemanRole = new IdentityRole("Pracownik sortowni");

                if (!dbContext.Roles.Any())
                {
                    roleMgr.CreateAsync(managerRole).GetAwaiter().GetResult();
                    roleMgr.CreateAsync(courierRole).GetAwaiter().GetResult();
                    roleMgr.CreateAsync(branchEmployeeRole).GetAwaiter().GetResult();
                    roleMgr.CreateAsync(clientRole).GetAwaiter().GetResult();
                    roleMgr.CreateAsync(warehousemanRole).GetAwaiter().GetResult();
                }

                if (!dbContext.Users.Any(u => u.UserName == "kierownik"))
                {
                    string password = "Hasło123!";

                    var managerUser = new IdentityUser
                    {
                        UserName = "kierownik@poczta.pl",
                        Email = "kierownik@poczta.pl"
                    };

                    var courierUser = new IdentityUser
                    {
                        UserName = "kurier@poczta.pl",
                        Email = "kurier@poczta.pl"
                    };

                    var branchEmployeeUser = new IdentityUser
                    {
                        UserName = "pracownik_oddzialu@poczta.pl",
                        Email = "pracownik_oddzialu@poczta.pl"
                    };

                    var clientUser = new IdentityUser
                    {
                        UserName = "klient@poczta.pl",
                        Email = "klient@poczta.pl"
                    };

                    var warehousemanUser = new IdentityUser
                    {
                        UserName = "pracownik_sortowni@poczta.pl",
                        Email = "pracownik_sortowni@poczta.pl"
                    };

                    var resultManager = userMgr.CreateAsync(managerUser, password).GetAwaiter().GetResult();
                    userMgr.AddToRoleAsync(managerUser, managerRole.Name).GetAwaiter().GetResult();

                    var resultCourier = userMgr.CreateAsync(courierUser, password).GetAwaiter().GetResult();
                    userMgr.AddToRoleAsync(courierUser, courierRole.Name).GetAwaiter().GetResult();

                    var resultBranchEmployee = userMgr.CreateAsync(branchEmployeeUser, password).GetAwaiter().GetResult();
                    userMgr.AddToRoleAsync(branchEmployeeUser, branchEmployeeRole.Name).GetAwaiter().GetResult();

                    var resultClient = userMgr.CreateAsync(clientUser, password).GetAwaiter().GetResult();
                    userMgr.AddToRoleAsync(clientUser, clientRole.Name).GetAwaiter().GetResult();

                    var resultwarehouseMan = userMgr.CreateAsync(warehousemanUser, password).GetAwaiter().GetResult();
                    userMgr.AddToRoleAsync(warehousemanUser, warehousemanRole.Name).GetAwaiter().GetResult();
                }
            }
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

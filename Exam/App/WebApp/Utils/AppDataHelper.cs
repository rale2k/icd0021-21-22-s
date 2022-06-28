using System.Security.Claims;
using App.DAL;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Utils;

public static class AppDataHelper
{
    public static void SetupAppData(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        using var context = serviceScope
            .ServiceProvider.GetService<AppDbContext>();

        if (context == null)
        {
            throw new ApplicationException("Problem in services. No db context.");
        }

        // TODO - Check database state
        // can't connect - wrong address
        // can't connect - wrong user/pass
        // can connect - but no database
        // can connect - there is database

        if (configuration.GetValue<bool>("DataInitialization:DropDatabase"))
        {
            context.Database.EnsureDeleted();
        }
        if (configuration.GetValue<bool>("DataInitialization:MigrateDatabase"))
        {
            context.Database.Migrate();
        }
        if (configuration.GetValue<bool>("DataInitialization:SeedIdentity"))
        {
            SeedIdentity(serviceScope);
        }
        if (configuration.GetValue<bool>("DataInitialization:SeedData"))
        {
            SeedData(serviceScope);
        }
    }

    private static void SeedIdentity(IServiceScope serviceScope)
    {
        var roles = new (string name, string displayName)[]
        {
            ("admin", "Administrator"),
            ("user", "User"),
        };
            
        var users = new (string username, string firstName, string lastName, string password, string roles)[]
        {
            ("qwe@qwe.qwe", "Admin", "Man", "qweqwe", "user,admin"),
            ("zxc@zcx.zxc", "Admin", "Man2", "zxczxc", "user,admin"),
            ("asd@asd.asd", "Hotelowner", "Man", "asdasd", "user"),
        };

        using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
        using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();

        if (userManager == null || roleManager == null)
        {
            throw new NullReferenceException("userManager or roleManager cannot be null!");
        }
        
        foreach (var roleInfo in roles)
        {
            var role = roleManager.FindByNameAsync(roleInfo.name).Result;
            if (role == null)
            {
                var identityResult = roleManager.CreateAsync(new AppRole()
                {
                    Name = roleInfo.name,
                }).Result;
                if (!identityResult.Succeeded)
                {
                    throw new ApplicationException("Role creation failed");
                }
            }
        }
        
        foreach (var userInfo in users)
        {
            var user = userManager.FindByEmailAsync(userInfo.username).Result;
            if (user == null)
            {
                user = new AppUser()
                {
                    Email = userInfo.username,
                    FirstName = userInfo.firstName,
                    LastName = userInfo.lastName,
                    UserName = userInfo.username,
                    EmailConfirmed = false,
                };
                
                var identityResult = userManager.CreateAsync(user, userInfo.password).Result;
                identityResult = userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName,user.FirstName)).Result;
                identityResult = userManager.AddClaimAsync(user, new Claim(ClaimTypes.Surname,user.LastName)).Result;

                if (!identityResult.Succeeded)
                {
                    throw new ApplicationException("Cannot create user!");
                }
            }

            if (!string.IsNullOrWhiteSpace(userInfo.roles))
            {
                var identityResultRole = userManager.AddToRolesAsync(user,
                    userInfo.roles.Split(",").Select(r => r.Trim())
                ).Result;
            }
        }
    }
    
    private static void SeedData(IServiceScope serviceScope)
    {
        // TODO - seeddata
    }
}
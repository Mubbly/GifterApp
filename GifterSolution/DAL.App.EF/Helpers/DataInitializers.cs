using System;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Helpers
{
    public class DataInitializers
    {
        public static void MigrateDatabase(AppDbContext context)
        {
            context.Database.Migrate();
        }
        public static bool DeleteDatabase(AppDbContext context)
        {
            return context.Database.EnsureDeleted();
        }
        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var roleNames = new string[] {"User", "Admin", "CampaignManager"};
            foreach (var roleName in roleNames)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new AppRole { Name = roleName };
                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Role not created!");
                    }
                }
            }

            var username = "test@test.com";
            var password = "Test123!";
            var firstName = "Test";
            var lastName = "Tester";
            
            var user = userManager.FindByNameAsync(username).Result;
            if (user == null)
            {
                user = new AppUser
                {
                    Email = username, 
                    UserName = username, 
                    FirstName = firstName, 
                    LastName = lastName
                };
                var result = userManager.CreateAsync(user, password).Result;
                if(!result.Succeeded)
                {
                    throw new ApplicationException("User creation failed!");
                }
            }

            var roleResult = userManager.AddToRoleAsync(user, "Admin").Result;
            roleResult = userManager.AddToRoleAsync(user, "CampaignManager").Result;
            roleResult = userManager.AddToRoleAsync(user, "User").Result;

        }
        
        public static void SeedData(AppDbContext context)
        {
        }
        
    }
}
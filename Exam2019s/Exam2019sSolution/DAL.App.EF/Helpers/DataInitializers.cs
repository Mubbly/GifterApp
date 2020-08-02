using System;
using Domain.App.Identity;
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
            var roles = new string[] {"user", "admin"};

            foreach (var roleName in roles)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new AppRole
                    {
                        Name = roleName
                    };

                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded) throw new ApplicationException("Role creation failed for: " + roleName);
                }
            }

            var users = new (string email, string username, string password, string firstName, string lastName, Guid Id)[]
            {
                ("test@test.com", "TestAccount", "Test123!", "Test", "Tester", new Guid("00000000-0000-0000-0000-000000000001"))
            };

            foreach (var userInfo in users)
            {
                var user = userManager.FindByNameAsync(userInfo.username).Result;
                if (user == null)
                {
                    user = new AppUser
                    {
                        Email = userInfo.email,
                        UserName = userInfo.username,
                        FirstName = userInfo.firstName,
                        LastName = userInfo.lastName
                    };
                    var result = userManager.CreateAsync(user, userInfo.password).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed for: " + userInfo.username);
                    }
                }

                var roleResult = userManager.AddToRoleAsync(user, "admin").Result;
                roleResult = userManager.AddToRoleAsync(user, "user").Result;
            }
        }

        public static void SeedData(AppDbContext context, UserManager<AppUser> userManager)
        {
            // Example:
            // Insert predefined actionTypes
            // var actionTypes = new[]
            // {
            //     new ActionType
            //     {
            //         ActionTypeValue = "Activate",
            //         Comment = "Change from reserved or archived status to active status",
            //         Id = new Guid("00000000-0000-0000-0000-000000000001")
            //     },
            //     new ActionType
            //     {
            //         ActionTypeValue = "Reserve",
            //         Comment = "Change from active status to reserved status",
            //         Id = new Guid("00000000-0000-0000-0000-000000000002")
            //     },
            //     new ActionType
            //     {
            //         ActionTypeValue = "Archive",
            //         Comment = "Move from reserved or active status to archived status",
            //         Id = new Guid("00000000-0000-0000-0000-000000000003")
            //     }
            // };
            //
            // foreach (var actionType in actionTypes)
            //     if (!context.ActionTypes.Any(a => a.Id == actionType.Id))
            //         context.ActionTypes.Add(actionType);
            // context.SaveChanges();

            // EXAMPLE for when test user depends on some predefined data:
            // Test user related data
            // var user = userManager.FindByEmailAsync("test@test.com").Result;
            // if (user != null)
            // {
            //     var testUserWishlist = new Wishlist
            //     {
            //         Id = new Guid("00000000-0000-0000-0000-000000000001"),
            //         AppUserId = user!.Id,
            //         Comment = "Test wishlist"
            //     };
            //
            //     if (!context.Wishlists.Any(w => w.Id == testUserWishlist.Id))
            //     {
            //         context.Wishlists.Add(testUserWishlist);
            //     }
            //     context.SaveChanges();
            //
            //     var testUserProfile = new Profile
            //     {
            //         Id = new Guid("00000000-0000-0000-0000-000000000001"),
            //         WishlistId = new Guid("00000000-0000-0000-0000-000000000001"),
            //         AppUserId = user!.Id,
            //         Age = 70,
            //         Gender = "Female",
            //         Bio =
            //             "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
            //             "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
            //             "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
            //             "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " +
            //             "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
            //     };
            //     if (!context.Profiles.Any(p => p.Id == testUserProfile.Id))
            //     {
            //         context.Profiles.Add(testUserProfile);
            //     }
            //     context.SaveChanges();
            // }
        }
    }
}
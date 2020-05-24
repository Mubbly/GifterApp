using System;
using System.Linq;
using Domain.App;
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
            var roles = new string[] {"user", "admin", "campaignManager"};

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
                roleResult = userManager.AddToRoleAsync(user, "campaignManager").Result;
                roleResult = userManager.AddToRoleAsync(user, "user").Result;
            }
        }

        public static void SeedData(AppDbContext context, UserManager<AppUser> userManager)
        {
            // Insert predefined actionTypes
            var actionTypes = new[]
            {
                new ActionType
                {
                    ActionTypeValue = "Activate",
                    Comment = "Change from reserved or archived status to active status",
                    Id = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new ActionType
                {
                    ActionTypeValue = "Reserve",
                    Comment = "Change from active status to reserved status",
                    Id = new Guid("00000000-0000-0000-0000-000000000002")
                },
                new ActionType
                {
                    ActionTypeValue = "Archive",
                    Comment = "Move from reserved or active status to archived status",
                    Id = new Guid("00000000-0000-0000-0000-000000000003")
                }
            };

            foreach (var actionType in actionTypes)
                if (!context.ActionTypes.Any(a => a.Id == actionType.Id))
                    context.ActionTypes.Add(actionType);
            context.SaveChanges();

            // Insert predefined statuses
            var statuses = new[]
            {
                new Status
                {
                    StatusValue = "Active",
                    Comment = "Not reserved or archived yet - still available for everyone",
                    Id = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new Status
                {
                    StatusValue = "Reserved",
                    Comment =
                        "Reserved by someone, unavailable to others (unless cancelled by reserver). Note: Receiver should still see active status.",
                    Id = new Guid("00000000-0000-0000-0000-000000000002")
                },
                new Status
                {
                    StatusValue = "Archived",
                    Comment =
                        "Already gifted or disregarded - unavailable for everyone (unless activated again by the receiver)",
                    Id = new Guid("00000000-0000-0000-0000-000000000003")
                }
            };

            foreach (var status in statuses)
                if (!context.Statuses.Any(s => s.Id == status.Id))
                    context.Statuses.Add(status);
            context.SaveChanges();

            // Insert predefined notificationTypes
            var notificationTypes = new[]
            {
                new NotificationType
                {
                    NotificationTypeValue = "Reminder",
                    Comment = "User has not interacted with certain things for a while",
                    Id = new Guid("00000000-0000-0000-0000-000000000001")
                },
                new NotificationType
                {
                    NotificationTypeValue = "NewFriendRequest",
                    Comment = "User has unconfirmed friend requests from other users",
                    Id = new Guid("00000000-0000-0000-0000-000000000002")
                },
                new NotificationType
                {
                    NotificationTypeValue = "NewMessage",
                    Comment = "User has unread private messages",
                    Id = new Guid("00000000-0000-0000-0000-000000000003")
                },
                new NotificationType
                {
                    NotificationTypeValue = "NewCampaign",
                    Comment = "There exists a new campaign that the user could participate in",
                    Id = new Guid("00000000-0000-0000-0000-000000000004")
                },
                new NotificationType
                {
                    NotificationTypeValue = "GiftSent",
                    Comment = "User has claimed they gave a gift to another user",
                    Id = new Guid("00000000-0000-0000-0000-000000000005")
                },
                new NotificationType
                {
                    NotificationTypeValue = "GiftReceived",
                    Comment = "User has received a gift from another user",
                    Id = new Guid("00000000-0000-0000-0000-000000000006")
                },
                new NotificationType
                {
                    NotificationTypeValue = "Reserved",
                    Comment = "User has reserved something",
                    Id = new Guid("00000000-0000-0000-0000-000000000007")
                },
                new NotificationType
                {
                    NotificationTypeValue = "Archived",
                    Comment = "Something has been archived",
                    Id = new Guid("00000000-0000-0000-0000-000000000008")
                },
                new NotificationType
                {
                    NotificationTypeValue = "Reactivated",
                    Comment = "Something reserved or archived has been re-activated",
                    Id = new Guid("00000000-0000-0000-0000-000000000009")
                }
            };

            foreach (var notificationType in notificationTypes)
                if (!context.NotificationTypes.Any(n => n.Id == notificationType.Id))
                    context.NotificationTypes.Add(notificationType);
            context.SaveChanges();
            
            // Test user related data

            var user = userManager.FindByEmailAsync("test@test.com").Result;
            if (user != null)
            {
                var testUserWishlist = new Wishlist
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    AppUserId = user!.Id,
                    Comment = "Test wishlist"
                };
            
                if (!context.Wishlists.Any(w => w.Id == testUserWishlist.Id))
                {
                    context.Wishlists.Add(testUserWishlist);
                }
                context.SaveChanges();
            
                var testUserProfile = new Profile
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    WishlistId = new Guid("00000000-0000-0000-0000-000000000001"),
                    AppUserId = user!.Id,
                    Age = 70,
                    Gender = "Female",
                    Bio =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                        "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                        "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. " +
                        "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " +
                        "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                };
                if (!context.Profiles.Any(p => p.Id == testUserProfile.Id))
                {
                    context.Profiles.Add(testUserProfile);
                }
                context.SaveChanges();
            }
        }
    }
}
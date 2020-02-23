using System;
using System.Runtime.CompilerServices;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext: IdentityDbContext<AppUser, AppRole, string>
    {
        public DbSet<ActionType> ActionTypes { get; set; } = default!;
        public DbSet<ArchivedGift> ArchivedGifts { get; set; } = default!;
        public DbSet<Campaign> Campaigns { get; set; } = default!;
        public DbSet<CampaignDonatee> CampaignDonatees { get; set; } = default!;
        public DbSet<Donatee> Donatees { get; set; } = default!;
        public DbSet<Friendship> Friendships { get; set; } = default!;
        public DbSet<Gift> Gifts { get; set; } = default!;
        public DbSet<InvitedUser> InvitedUsers { get; set; } = default!;
        public DbSet<Notification> Notifications { get; set; } = default!;
        public DbSet<NotificationType> NotificationTypes { get; set; } = default!;
        public DbSet<Permission> Permissions { get; set; } = default!;
        public DbSet<PrivateMessage> PrivateMessages { get; set; } = default!;
        public DbSet<Profile> Profiles { get; set; } = default!;
        public DbSet<ReservedGift> ReservedGifts { get; set; } = default!;
        public DbSet<Status> Statuses { get; set; } = default!;
        public DbSet<UserCampaign> UserCampaigns { get; set; } = default!;
        public DbSet<UserNotification> UserNotifications { get; set; } = default!;
        public DbSet<UserPermission> UserPermissions { get; set; } = default!;
        public DbSet<UserProfile> UserProfiles { get; set; } = default!;
        public DbSet<Wishlist> Wishlists { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
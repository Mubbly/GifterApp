using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext: IdentityDbContext<AppUser, AppRole, Guid>
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
        
         protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);

            // This code is commented out because it only applies when using MSSQL and still had errors
            
            // Following code is to turn off Cascade Delete for relationships that have multiple FK references to the SAME table
            // to avoid cycles and multiple cascade paths
            // TODO: Remove repetitions, create general logic to target them at once
            
            /*
                // Turn off cascade delete for every foreign key relationship
                foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                {
                    relationship.DeleteBehavior = DeleteBehavior.Restrict;
                }
            */
            
            /*modelBuilder.Entity<Friendship>()
                .HasOne(f => f.AppUser1)
                .WithMany(t => t.ConfirmedFriendships)
                .HasForeignKey(t => t.AppUser1Id)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.AppUser2)
                .WithMany(t => t.PendingFriendships)
                .HasForeignKey(t => t.AppUser2Id)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<PrivateMessage>()
                .HasOne(f => f.UserReceiver)
                .WithMany(t => t.ReceivedPrivateMessages)
                .HasForeignKey(t => t.UserReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PrivateMessage>()
                .HasOne(f => f.UserSender)
                .WithMany(t => t.SentPrivateMessages)
                .HasForeignKey(t => t.UserSenderId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<PrivateMessage>()
                .HasOne(f => f.UserReceiver)
                .WithMany(t => t.ReceivedPrivateMessages)
                .HasForeignKey(t => t.UserReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PrivateMessage>()
                .HasOne(f => f.UserSender)
                .WithMany(t => t.SentPrivateMessages)
                .HasForeignKey(t => t.UserSenderId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<ReservedGift>()
                .HasOne(f => f.UserReceiver)
                .WithMany(t => t.ReservedGiftsForUser)
                .HasForeignKey(t => t.UserReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ReservedGift>()
                .HasOne(f => f.UserGiver)
                .WithMany(t => t.ReservedGiftsByUser)
                .HasForeignKey(t => t.UserGiverId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ArchivedGift>()
                .HasOne(f => f.UserReceiver)
                .WithMany(t => t.ArchivedGiftsForUser)
                .HasForeignKey(t => t.UserReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ArchivedGift>()
                .HasOne(f => f.UserGiver)
                .WithMany(t => t.ArchivedGiftsByUser)
                .HasForeignKey(t => t.UserGiverId)
                .OnDelete(DeleteBehavior.NoAction);*/
        }
    }
}
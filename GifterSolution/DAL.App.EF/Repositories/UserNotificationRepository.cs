using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class UserNotificationRepository : EFBaseRepository<UserNotification, AppDbContext>, IUserNotificationRepository
    {
        public UserNotificationRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<UserNotification>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(un => un.AppUser)
                .Include(un => un.Notification)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(un => un.AppUserId == userId);
            }

            return await query.ToListAsync();
        }

        public async Task<UserNotification> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(un => un.Id == id)
                .Include(un => un.AppUser)
                .Include(un => un.Notification)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(un => un.AppUserId == userId);
            }
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId != null)
            {
                return await RepoDbSet.AnyAsync(un => un.Id == id && un.AppUserId == userId);
            }
            return await RepoDbSet.AnyAsync(un => un.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var userNotification = await FirstOrDefaultAsync(id, userId);
            base.Remove(userNotification);
        }

        public async Task<IEnumerable<UserNotificationDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(un => un.AppUser)
                .Include(un => un.Notification)
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(un => un.AppUserId == userId);
            }

            return await query
                .Select(un => new UserNotificationDTO()
                {
                    Id = un.Id,
                    LastNotified = un.LastNotified,
                    RenotifyAt = un.RenotifyAt,
                    IsActive = un.IsActive,
                    IsDisabled = un.IsDisabled,
                    Comment = un.Comment,
                    AppUserId = un.AppUserId,
                    NotificationId = un.NotificationId,
                    AppUser = new AppUserDTO()
                    {
                        Id = un.AppUser!.Id,
                        FirstName = un.AppUser!.FirstName,
                        LastName = un.AppUser!.LastName,
                        IsCampaignManager = un.AppUser!.IsCampaignManager,
                        IsActive = un.AppUser!.IsActive,
                        LastActive = un.AppUser!.LastActive,
                        DateJoined = un.AppUser!.DateJoined,
                        UserPermissionsCount = un.AppUser!.UserPermissions.Count,
                        UserProfilesCount = un.AppUser!.UserProfiles.Count,
                        UserCampaignsCount = un.AppUser!.UserCampaigns.Count,
                        UserNotificationsCount = un.AppUser!.UserNotifications.Count,
                        GiftsCount = un.AppUser!.Gifts.Count,
                        ReservedGiftsByUserCount = un.AppUser!.ReservedGiftsByUser.Count,
                        ReservedGiftsForUserCount = un.AppUser!.ReservedGiftsForUser.Count,
                        ArchivedGiftsByUserCount = un.AppUser!.ArchivedGiftsByUser.Count,
                        ArchivedGiftsForUserCount = un.AppUser!.ArchivedGiftsForUser.Count,
                        ConfirmedFriendshipsCount = un.AppUser!.ConfirmedFriendships.Count,
                        PendingFriendshipsCount = un.AppUser!.PendingFriendships.Count,
                        SentPrivateMessagesCount = un.AppUser!.SentPrivateMessages.Count,
                        ReceivedPrivateMessagesCount = un.AppUser!.ReceivedPrivateMessages.Count,
                        InvitedUsersCount = un.AppUser!.InvitedUsers.Count
                    },
                    Notification = new NotificationDTO()
                    {
                        Id = un.Notification!.Id,
                        NotificationValue = un.Notification!.NotificationValue,
                        Comment = un.Notification!.Comment,
                        NotificationTypeId = un.Notification!.NotificationTypeId,
                        UserNotificationsCount = un.Notification!.UserNotifications.Count,
                        NotificationType = new NotificationTypeDTO()
                        {
                            Id = un.Notification!.NotificationType!.Id,
                            NotificationTypeValue = un.Notification!.NotificationType!.NotificationTypeValue,
                            Comment = un.Notification!.NotificationType!.Comment,
                            NotificationsCount = un.Notification!.NotificationType!.Notifications.Count
                        }
                        
                    }
                }).ToListAsync();
        }

        public async Task<UserNotificationDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(un => un.Id == id)
                .Include(un => un.AppUser)
                .Include(un => un.Notification)
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(un => un.AppUserId == userId);
            }

            return await query.Select(un => new UserNotificationDTO()
            {
                Id = un.Id,
                LastNotified = un.LastNotified,
                RenotifyAt = un.RenotifyAt,
                IsActive = un.IsActive,
                IsDisabled = un.IsDisabled,
                Comment = un.Comment,
                AppUserId = un.AppUserId,
                NotificationId = un.NotificationId,
                AppUser = new AppUserDTO()
                {
                    Id = un.AppUser!.Id,
                    FirstName = un.AppUser!.FirstName,
                    LastName = un.AppUser!.LastName,
                    IsCampaignManager = un.AppUser!.IsCampaignManager,
                    IsActive = un.AppUser!.IsActive,
                    LastActive = un.AppUser!.LastActive,
                    DateJoined = un.AppUser!.DateJoined,
                    UserPermissionsCount = un.AppUser!.UserPermissions.Count,
                    UserProfilesCount = un.AppUser!.UserProfiles.Count,
                    UserCampaignsCount = un.AppUser!.UserCampaigns.Count,
                    UserNotificationsCount = un.AppUser!.UserNotifications.Count,
                    GiftsCount = un.AppUser!.Gifts.Count,
                    ReservedGiftsByUserCount = un.AppUser!.ReservedGiftsByUser.Count,
                    ReservedGiftsForUserCount = un.AppUser!.ReservedGiftsForUser.Count,
                    ArchivedGiftsByUserCount = un.AppUser!.ArchivedGiftsByUser.Count,
                    ArchivedGiftsForUserCount = un.AppUser!.ArchivedGiftsForUser.Count,
                    ConfirmedFriendshipsCount = un.AppUser!.ConfirmedFriendships.Count,
                    PendingFriendshipsCount = un.AppUser!.PendingFriendships.Count,
                    SentPrivateMessagesCount = un.AppUser!.SentPrivateMessages.Count,
                    ReceivedPrivateMessagesCount = un.AppUser!.ReceivedPrivateMessages.Count,
                    InvitedUsersCount = un.AppUser!.InvitedUsers.Count
                },
                Notification = new NotificationDTO()
                {
                    Id = un.Notification!.Id,
                    NotificationValue = un.Notification!.NotificationValue,
                    Comment = un.Notification!.Comment,
                    NotificationTypeId = un.Notification!.NotificationTypeId,
                    UserNotificationsCount = un.Notification!.UserNotifications.Count,
                    NotificationType = new NotificationTypeDTO()
                    {
                        Id = un.Notification!.NotificationType!.Id,
                        NotificationTypeValue = un.Notification!.NotificationType!.NotificationTypeValue,
                        Comment = un.Notification!.NotificationType!.Comment,
                        NotificationsCount = un.Notification!.NotificationType!.Notifications.Count
                    }
                    
                }
            }).FirstOrDefaultAsync();
        }
    }
}
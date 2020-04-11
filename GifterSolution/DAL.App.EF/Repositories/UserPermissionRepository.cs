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
    public class UserPermissionRepository : EFBaseRepository<UserPermission, AppDbContext>, IUserPermissionRepository
    {
        public UserPermissionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
                
        public async Task<IEnumerable<UserPermission>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(up => up.AppUser)
                .Include(up => up.Permission)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(up => up.AppUserId == userId);
            }

            return await query.ToListAsync();
        }

        public async Task<UserPermission> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(up => up.Id == id)
                .Include(up => up.AppUser)
                .Include(up => up.Permission)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(up => up.AppUserId == userId);
            }
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId != null)
            {
                return await RepoDbSet.AnyAsync(up => up.Id == id && up.AppUserId == userId);
            }
            return await RepoDbSet.AnyAsync(up => up.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var userPermission = await FirstOrDefaultAsync(id, userId);
            base.Remove(userPermission);
        }

        public async Task<IEnumerable<UserPermissionDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(up => up.AppUser)
                .Include(up => up.Permission)
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(up => up.AppUserId == userId);
            }

            return await query
                .Select(up => new UserPermissionDTO()
                {
                    Id = up.Id,
                    From = up.From,
                    To = up.To,
                    Comment = up.Comment,
                    AppUserId = up.AppUserId,
                    PermissionId = up.PermissionId,
                    AppUser = new AppUserDTO()
                    {
                        Id = up.AppUser!.Id,
                        FirstName = up.AppUser!.FirstName,
                        LastName = up.AppUser!.LastName,
                        IsCampaignManager = up.AppUser!.IsCampaignManager,
                        IsActive = up.AppUser!.IsActive,
                        LastActive = up.AppUser!.LastActive,
                        DateJoined = up.AppUser!.DateJoined,
                        UserPermissionsCount = up.AppUser!.UserPermissions.Count,
                        UserProfilesCount = up.AppUser!.UserProfiles.Count,
                        UserCampaignsCount = up.AppUser!.UserCampaigns.Count,
                        UserNotificationsCount = up.AppUser!.UserNotifications.Count,
                        GiftsCount = up.AppUser!.Gifts.Count,
                        ReservedGiftsByUserCount = up.AppUser!.ReservedGiftsByUser.Count,
                        ReservedGiftsForUserCount = up.AppUser!.ReservedGiftsForUser.Count,
                        ArchivedGiftsByUserCount = up.AppUser!.ArchivedGiftsByUser.Count,
                        ArchivedGiftsForUserCount = up.AppUser!.ArchivedGiftsForUser.Count,
                        ConfirmedFriendshipsCount = up.AppUser!.ConfirmedFriendships.Count,
                        PendingFriendshipsCount = up.AppUser!.PendingFriendships.Count,
                        SentPrivateMessagesCount = up.AppUser!.SentPrivateMessages.Count,
                        ReceivedPrivateMessagesCount = up.AppUser!.ReceivedPrivateMessages.Count,
                        InvitedUsersCount = up.AppUser!.InvitedUsers.Count
                    },
                    Permission = new PermissionDTO()
                    {
                        Id = up.Permission!.Id,
                        PermissionValue = up.Permission!.PermissionValue,
                        Comment = up.Permission!.Comment,
                        UserPermissionsCount = up.Permission!.UserPermissions.Count
                    }
                }).ToListAsync();
        }

        public async Task<UserPermissionDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(up => up.Id == id)
                .Include(up => up.AppUser)
                .Include(up => up.Permission)
                .AsQueryable();
            if (userId != null)
            {
                query = query.Where(up => up.AppUserId == userId);
            }
            
            return await query.Select(up => new UserPermissionDTO()
            {
                Id = up.Id,
                From = up.From,
                To = up.To,
                Comment = up.Comment,
                AppUserId = up.AppUserId,
                PermissionId = up.PermissionId,
                AppUser = new AppUserDTO()
                {
                    Id = up.AppUser!.Id,
                    FirstName = up.AppUser!.FirstName,
                    LastName = up.AppUser!.LastName,
                    IsCampaignManager = up.AppUser!.IsCampaignManager,
                    IsActive = up.AppUser!.IsActive,
                    LastActive = up.AppUser!.LastActive,
                    DateJoined = up.AppUser!.DateJoined,
                    UserPermissionsCount = up.AppUser!.UserPermissions.Count,
                    UserProfilesCount = up.AppUser!.UserProfiles.Count,
                    UserCampaignsCount = up.AppUser!.UserCampaigns.Count,
                    UserNotificationsCount = up.AppUser!.UserNotifications.Count,
                    GiftsCount = up.AppUser!.Gifts.Count,
                    ReservedGiftsByUserCount = up.AppUser!.ReservedGiftsByUser.Count,
                    ReservedGiftsForUserCount = up.AppUser!.ReservedGiftsForUser.Count,
                    ArchivedGiftsByUserCount = up.AppUser!.ArchivedGiftsByUser.Count,
                    ArchivedGiftsForUserCount = up.AppUser!.ArchivedGiftsForUser.Count,
                    ConfirmedFriendshipsCount = up.AppUser!.ConfirmedFriendships.Count,
                    PendingFriendshipsCount = up.AppUser!.PendingFriendships.Count,
                    SentPrivateMessagesCount = up.AppUser!.SentPrivateMessages.Count,
                    ReceivedPrivateMessagesCount = up.AppUser!.ReceivedPrivateMessages.Count,
                    InvitedUsersCount = up.AppUser!.InvitedUsers.Count
                },
                Permission = new PermissionDTO()
                {
                    Id = up.Permission!.Id,
                    PermissionValue = up.Permission!.PermissionValue,
                    Comment = up.Permission!.Comment,
                    UserPermissionsCount = up.Permission!.UserPermissions.Count
                }
            }).FirstOrDefaultAsync();
        }
    }
}
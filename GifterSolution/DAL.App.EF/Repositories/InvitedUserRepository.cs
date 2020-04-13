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
    public class InvitedUserRepository : EFBaseRepository<InvitedUser, AppDbContext>, IInvitedUserRepository
    {
        public InvitedUserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<IEnumerable<InvitedUser>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(iu => iu.InvitorUser)
                .AsQueryable();

            if (userId != null)
            {
                // See only who you have invited
                query = query.Where(iu => iu.InvitorUserId == userId);
            }
            return await query.ToListAsync();
        }

        public async Task<InvitedUser> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(iu => iu.InvitorUser)
                .Where(iu => iu.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                // See only who you have invited
                query = query.Where(iu => iu.InvitorUserId == userId);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId != null)
            {
                return await RepoDbSet.AnyAsync(iu => iu.Id == id && iu.InvitorUserId == userId);
            }
            return await RepoDbSet.AnyAsync(iu => iu.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var invitorUser = await FirstOrDefaultAsync(id, userId);
            base.Remove(invitorUser);
        }

        public async Task<IEnumerable<InvitedUserDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(iu => iu.InvitorUser)
                .AsQueryable();

            if (userId != null)
            {
                // See only who you have invited
                query = query.Where(iu => iu.InvitorUserId == userId);
            }
            return await query
                .Select(iu => new InvitedUserDTO
                {
                    Id = iu.Id,
                    Email = iu.Email,
                    PhoneNumber = iu.PhoneNumber,
                    Message = iu.Message,
                    DateInvited = iu.DateInvited,
                    HasJoined = iu.HasJoined,
                    InvitorUserId = iu.InvitorUserId,
                    InvitorUser = new AppUserDTO
                    {
                        Id = iu.InvitorUser!.Id,
                        FirstName = iu.InvitorUser!.FirstName,
                        LastName = iu.InvitorUser!.LastName,
                        IsCampaignManager = iu.InvitorUser!.IsCampaignManager,
                        IsActive = iu.InvitorUser!.IsActive,
                        LastActive = iu.InvitorUser!.LastActive,
                        DateJoined = iu.InvitorUser!.DateJoined,
                        UserPermissionsCount = iu.InvitorUser!.UserPermissions.Count,
                        UserProfilesCount = iu.InvitorUser!.UserProfiles.Count,
                        UserNotificationsCount = iu.InvitorUser!.UserNotifications.Count,
                        UserCampaignsCount = iu.InvitorUser!.UserCampaigns.Count,
                        GiftsCount = iu.InvitorUser!.Gifts.Count,
                        ReservedGiftsByUserCount = iu.InvitorUser!.ReservedGiftsByUser.Count,
                        ReservedGiftsForUserCount = iu.InvitorUser!.ReservedGiftsForUser.Count,
                        ArchivedGiftsByUserCount = iu.InvitorUser!.ArchivedGiftsByUser.Count,
                        ArchivedGiftsForUserCount = iu.InvitorUser!.ArchivedGiftsForUser.Count,
                        ConfirmedFriendshipsCount = iu.InvitorUser!.ConfirmedFriendships.Count,
                        PendingFriendshipsCount = iu.InvitorUser!.PendingFriendships.Count,
                        SentPrivateMessagesCount = iu.InvitorUser!.SentPrivateMessages.Count,
                        ReceivedPrivateMessagesCount = iu.InvitorUser!.ReceivedPrivateMessages.Count,
                        InvitedUsersCount = iu.InvitorUser!.InvitedUsers.Count   
                    }
                }).ToListAsync();
        }

        public async Task<InvitedUserDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(iu => iu.InvitorUser)
                .Where(iu => iu.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                // See only who you have invited
                query = query.Where(iu => iu.InvitorUserId == userId);
            }
            return await query
                .Select(iu => new InvitedUserDTO
                {
                    Id = iu.Id,
                    Email = iu.Email,
                    PhoneNumber = iu.PhoneNumber,
                    Message = iu.Message,
                    DateInvited = iu.DateInvited,
                    HasJoined = iu.HasJoined,
                    InvitorUserId = iu.InvitorUserId,
                    InvitorUser = new AppUserDTO
                    {
                        Id = iu.InvitorUser!.Id,
                        FirstName = iu.InvitorUser!.FirstName,
                        LastName = iu.InvitorUser!.LastName,
                        IsCampaignManager = iu.InvitorUser!.IsCampaignManager,
                        IsActive = iu.InvitorUser!.IsActive,
                        LastActive = iu.InvitorUser!.LastActive,
                        DateJoined = iu.InvitorUser!.DateJoined,
                        UserPermissionsCount = iu.InvitorUser!.UserPermissions.Count,
                        UserProfilesCount = iu.InvitorUser!.UserProfiles.Count,
                        UserNotificationsCount = iu.InvitorUser!.UserNotifications.Count,
                        UserCampaignsCount = iu.InvitorUser!.UserCampaigns.Count,
                        GiftsCount = iu.InvitorUser!.Gifts.Count,
                        ReservedGiftsByUserCount = iu.InvitorUser!.ReservedGiftsByUser.Count,
                        ReservedGiftsForUserCount = iu.InvitorUser!.ReservedGiftsForUser.Count,
                        ArchivedGiftsByUserCount = iu.InvitorUser!.ArchivedGiftsByUser.Count,
                        ArchivedGiftsForUserCount = iu.InvitorUser!.ArchivedGiftsForUser.Count,
                        ConfirmedFriendshipsCount = iu.InvitorUser!.ConfirmedFriendships.Count,
                        PendingFriendshipsCount = iu.InvitorUser!.PendingFriendships.Count,
                        SentPrivateMessagesCount = iu.InvitorUser!.SentPrivateMessages.Count,
                        ReceivedPrivateMessagesCount = iu.InvitorUser!.ReceivedPrivateMessages.Count,
                        InvitedUsersCount = iu.InvitorUser!.InvitedUsers.Count   
                    }
                }).FirstOrDefaultAsync();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.mubbly.gifterapp.DAL.Base.EF.Repositories;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using Microsoft.EntityFrameworkCore;
using DomainApp = Domain.App;
using DALAppDTO = DAL.App.DTO;
using DomainAppIdentity = Domain.App.Identity;

namespace DAL.App.EF.Repositories
{
    public class InvitedUserRepository :
        EFBaseRepository<AppDbContext, DomainAppIdentity.AppUser, DomainApp.InvitedUser, DALAppDTO.InvitedUserDAL>,
        IInvitedUserRepository
    {
        public InvitedUserRepository(AppDbContext dbContext) :
            base(dbContext, new DALMapper<DomainApp.InvitedUser, DALAppDTO.InvitedUserDAL>())
        {
        }
        
        public async Task<IEnumerable<DALAppDTO.InvitedUserDAL>> GetAllPersonalAsync(Guid userId, bool noTracking = true)
        {
            // User's invitedUsers
            var invitedUsers = PrepareQuery(userId, noTracking);
            var personalInvitedUsers = 
                await invitedUsers
                    .Where(u => u.InvitorUserId == userId)
                    .OrderBy(e => e.CreatedAt)
                    .Select(e => Mapper.Map(e))
                    .ToListAsync();
            return personalInvitedUsers;
        }

        // public async Task<IEnumerable<InvitedUser>> AllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(iu => iu.InvitorUser)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // See only who you have invited
        //         query = query.Where(iu => iu.InvitorUserId == userId);
        //     }
        //     return await query.ToListAsync();
        // }
        //
        // public async Task<InvitedUser> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(iu => iu.InvitorUser)
        //         .Where(iu => iu.Id == id)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // See only who you have invited
        //         query = query.Where(iu => iu.InvitorUserId == userId);
        //     }
        //     return await query.FirstOrDefaultAsync();
        // }
        //
        // public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        // {
        //     if (userId != null)
        //     {
        //         return await RepoDbSet.AnyAsync(iu => iu.Id == id && iu.InvitorUserId == userId);
        //     }
        //     return await RepoDbSet.AnyAsync(iu => iu.Id == id);
        // }
        //
        // public async Task DeleteAsync(Guid id, Guid? userId = null)
        // {
        //     var invitorUser = await FirstOrDefaultAsync(id, userId);
        //     base.Remove(invitorUser);
        // }
        //
        // public async Task<IEnumerable<InvitedUserDTO>> DTOAllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(iu => iu.InvitorUser)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // See only who you have invited
        //         query = query.Where(iu => iu.InvitorUserId == userId);
        //     }
        //     return await query
        //         .Select(iu => new InvitedUserDTO
        //         {
        //             Id = iu.Id,
        //             Email = iu.Email,
        //             PhoneNumber = iu.PhoneNumber,
        //             Message = iu.Message,
        //             DateInvited = iu.DateInvited,
        //             HasJoined = iu.HasJoined,
        //             InvitorUserId = iu.InvitorUserId,
        //             InvitorUser = new AppUserDTO
        //             {
        //                 Id = iu.InvitorUser!.Id,
        //                 FirstName = iu.InvitorUser!.FirstName,
        //                 LastName = iu.InvitorUser!.LastName,
        //                 IsCampaignManager = iu.InvitorUser!.IsCampaignManager,
        //                 IsActive = iu.InvitorUser!.IsActive,
        //                 LastActive = iu.InvitorUser!.LastActive,
        //                 DateJoined = iu.InvitorUser!.DateJoined,
        //                 UserPermissionsCount = iu.InvitorUser!.UserPermissions.Count,
        //                 UserProfilesCount = iu.InvitorUser!.UserProfiles.Count,
        //                 UserNotificationsCount = iu.InvitorUser!.UserNotifications.Count,
        //                 UserCampaignsCount = iu.InvitorUser!.UserCampaigns.Count,
        //                 GiftsCount = iu.InvitorUser!.Gifts.Count,
        //                 ReservedGiftsByUserCount = iu.InvitorUser!.ReservedGiftsByUser.Count,
        //                 ReservedGiftsForUserCount = iu.InvitorUser!.ReservedGiftsForUser.Count,
        //                 ArchivedGiftsByUserCount = iu.InvitorUser!.ArchivedGiftsByUser.Count,
        //                 ArchivedGiftsForUserCount = iu.InvitorUser!.ArchivedGiftsForUser.Count,
        //                 ConfirmedInvitedUsersCount = iu.InvitorUser!.ConfirmedInvitedUsers.Count,
        //                 PendingInvitedUsersCount = iu.InvitorUser!.PendingInvitedUsers.Count,
        //                 SentPrivateMessagesCount = iu.InvitorUser!.SentPrivateMessages.Count,
        //                 ReceivedPrivateMessagesCount = iu.InvitorUser!.ReceivedPrivateMessages.Count,
        //                 InvitedUsersCount = iu.InvitorUser!.InvitedUsers.Count   
        //             }
        //         }).ToListAsync();
        // }
        //
        // public async Task<InvitedUserDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(iu => iu.InvitorUser)
        //         .Where(iu => iu.Id == id)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // See only who you have invited
        //         query = query.Where(iu => iu.InvitorUserId == userId);
        //     }
        //     return await query
        //         .Select(iu => new InvitedUserDTO
        //         {
        //             Id = iu.Id,
        //             Email = iu.Email,
        //             PhoneNumber = iu.PhoneNumber,
        //             Message = iu.Message,
        //             DateInvited = iu.DateInvited,
        //             HasJoined = iu.HasJoined,
        //             InvitorUserId = iu.InvitorUserId,
        //             InvitorUser = new AppUserDTO
        //             {
        //                 Id = iu.InvitorUser!.Id,
        //                 FirstName = iu.InvitorUser!.FirstName,
        //                 LastName = iu.InvitorUser!.LastName,
        //                 IsCampaignManager = iu.InvitorUser!.IsCampaignManager,
        //                 IsActive = iu.InvitorUser!.IsActive,
        //                 LastActive = iu.InvitorUser!.LastActive,
        //                 DateJoined = iu.InvitorUser!.DateJoined,
        //                 UserPermissionsCount = iu.InvitorUser!.UserPermissions.Count,
        //                 UserProfilesCount = iu.InvitorUser!.UserProfiles.Count,
        //                 UserNotificationsCount = iu.InvitorUser!.UserNotifications.Count,
        //                 UserCampaignsCount = iu.InvitorUser!.UserCampaigns.Count,
        //                 GiftsCount = iu.InvitorUser!.Gifts.Count,
        //                 ReservedGiftsByUserCount = iu.InvitorUser!.ReservedGiftsByUser.Count,
        //                 ReservedGiftsForUserCount = iu.InvitorUser!.ReservedGiftsForUser.Count,
        //                 ArchivedGiftsByUserCount = iu.InvitorUser!.ArchivedGiftsByUser.Count,
        //                 ArchivedGiftsForUserCount = iu.InvitorUser!.ArchivedGiftsForUser.Count,
        //                 ConfirmedInvitedUsersCount = iu.InvitorUser!.ConfirmedInvitedUsers.Count,
        //                 PendingInvitedUsersCount = iu.InvitorUser!.PendingInvitedUsers.Count,
        //                 SentPrivateMessagesCount = iu.InvitorUser!.SentPrivateMessages.Count,
        //                 ReceivedPrivateMessagesCount = iu.InvitorUser!.ReceivedPrivateMessages.Count,
        //                 InvitedUsersCount = iu.InvitorUser!.InvitedUsers.Count   
        //             }
        //         }).FirstOrDefaultAsync();
        // }
    }
}
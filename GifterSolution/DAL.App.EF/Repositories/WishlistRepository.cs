﻿using com.mubbly.gifterapp.DAL.Base.EF.Repositories;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DomainApp = Domain.App;
using DALAppDTO = DAL.App.DTO;
using DomainAppIdentity = Domain.App.Identity;

namespace DAL.App.EF.Repositories
{
    public class WishlistRepository :
        EFBaseRepository<AppDbContext, DomainAppIdentity.AppUser, DomainApp.Wishlist, DALAppDTO.WishlistDAL>,
        IWishlistRepository
    {
        public WishlistRepository(AppDbContext dbContext) :
            base(dbContext, new DALMapper<DomainApp.Wishlist, DALAppDTO.WishlistDAL>())
        {
        }

        // public async Task<IEnumerable<Wishlist>> AllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(w => w.AppUser)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // See your own wishlist
        //         query = query.Where(w => w.AppUserId == userId);
        //     }
        //
        //     return await query.ToListAsync();
        // }
        //
        // public async Task<Wishlist> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Where(w => w.Id == id)
        //         .Include(w => w.AppUser)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // See your own wishlist
        //         query = query.Where(w => w.AppUserId == userId);
        //     }
        //     
        //     return await query.FirstOrDefaultAsync();
        // }
        //
        // public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        // {
        //     if (userId != null)
        //     {
        //         return await RepoDbSet.AnyAsync(w => w.Id == id && w.AppUserId == userId);
        //     }
        //     return await RepoDbSet.AnyAsync(w => w.Id == id);
        // }
        //
        // public async Task DeleteAsync(Guid id, Guid? userId = null)
        // {
        //     var wishlist = await FirstOrDefaultAsync(id, userId);
        //     base.Remove(wishlist);
        // }
        //
        // public async Task<IEnumerable<WishlistDTO>> DTOAllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(w => w.AppUser)
        //         .AsQueryable();
        //     if (userId != null)
        //     {
        //         // See your own wishlist
        //         query = query.Where(w => w.AppUserId == userId);
        //     }
        //
        //     return await query
        //         .Select(w => new WishlistDTO()
        //             {
        //                 Id = w.Id,
        //                 Comment = w.Comment,
        //                 AppUserId = w.AppUserId,
        //                 GiftsCount = w.Gifts.Count,
        //                 ProfilesCount = w.Profiles.Count,
        //                 AppUser = new AppUserDTO()
        //                 {
        //                     Id = w.AppUser!.Id,
        //                     FirstName = w.AppUser!.FirstName,
        //                     LastName = w.AppUser!.LastName,
        //                     IsCampaignManager = w.AppUser!.IsCampaignManager,
        //                     IsActive = w.AppUser!.IsActive,
        //                     LastActive = w.AppUser!.LastActive,
        //                     DateJoined = w.AppUser!.DateJoined,
        //                     UserPermissionsCount = w.AppUser!.UserPermissions.Count,
        //                     UserProfilesCount = w.AppUser!.UserProfiles.Count,
        //                     UserNotificationsCount = w.AppUser!.UserNotifications.Count,
        //                     UserCampaignsCount = w.AppUser!.UserCampaigns.Count,
        //                     GiftsCount = w.AppUser!.Gifts.Count,
        //                     ReservedGiftsByUserCount = w.AppUser!.ReservedGiftsByUser.Count,
        //                     ReservedGiftsForUserCount = w.AppUser!.ReservedGiftsForUser.Count,
        //                     ArchivedGiftsByUserCount = w.AppUser!.ArchivedGiftsByUser.Count,
        //                     ArchivedGiftsForUserCount = w.AppUser!.ArchivedGiftsForUser.Count,
        //                     ConfirmedFriendshipsCount = w.AppUser!.ConfirmedFriendships.Count,
        //                     PendingFriendshipsCount = w.AppUser!.PendingFriendships.Count,
        //                     SentPrivateMessagesCount = w.AppUser!.SentPrivateMessages.Count,
        //                     ReceivedPrivateMessagesCount = w.AppUser!.ReceivedPrivateMessages.Count,
        //                     InvitedUsersCount = w.AppUser!.InvitedUsers.Count
        //                 }
        //         }).ToListAsync();
        // }
        //
        // public async Task<WishlistDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Where(w => w.Id == id)
        //         .Include(w => w.AppUser)
        //         .AsQueryable();
        //     if (userId != null)
        //     {
        //         // See your own wishlist
        //         query = query.Where(w => w.AppUserId == userId);
        //     }
        //     
        //     return await query.Select(w => new WishlistDTO()
        //         {
        //             Id = w.Id,
        //             Comment = w.Comment,
        //             AppUserId = w.AppUserId,
        //             GiftsCount = w.Gifts.Count,
        //             ProfilesCount = w.Profiles.Count,
        //             AppUser = new AppUserDTO()
        //             {
        //                 Id = w.AppUser!.Id,
        //                 FirstName = w.AppUser!.FirstName,
        //                 LastName = w.AppUser!.LastName,
        //                 IsCampaignManager = w.AppUser!.IsCampaignManager,
        //                 IsActive = w.AppUser!.IsActive,
        //                 LastActive = w.AppUser!.LastActive,
        //                 DateJoined = w.AppUser!.DateJoined,
        //                 UserPermissionsCount = w.AppUser!.UserPermissions.Count,
        //                 UserProfilesCount = w.AppUser!.UserProfiles.Count,
        //                 UserNotificationsCount = w.AppUser!.UserNotifications.Count,
        //                 UserCampaignsCount = w.AppUser!.UserCampaigns.Count,
        //                 GiftsCount = w.AppUser!.Gifts.Count,
        //                 ReservedGiftsByUserCount = w.AppUser!.ReservedGiftsByUser.Count,
        //                 ReservedGiftsForUserCount = w.AppUser!.ReservedGiftsForUser.Count,
        //                 ArchivedGiftsByUserCount = w.AppUser!.ArchivedGiftsByUser.Count,
        //                 ArchivedGiftsForUserCount = w.AppUser!.ArchivedGiftsForUser.Count,
        //                 ConfirmedFriendshipsCount = w.AppUser!.ConfirmedFriendships.Count,
        //                 PendingFriendshipsCount = w.AppUser!.PendingFriendships.Count,
        //                 SentPrivateMessagesCount = w.AppUser!.SentPrivateMessages.Count,
        //                 ReceivedPrivateMessagesCount = w.AppUser!.ReceivedPrivateMessages.Count,
        //                 InvitedUsersCount = w.AppUser!.InvitedUsers.Count
        //             }
        //         }).FirstOrDefaultAsync();
        // }
    }
}
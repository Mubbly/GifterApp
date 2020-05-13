using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using DomainApp = Domain.App;
using DALAppDTO = DAL.App.DTO;
using DomainAppIdentity = Domain.App.Identity;

namespace DAL.App.EF.Repositories
{
    public class GiftRepository :
        EFBaseRepository<AppDbContext, DomainAppIdentity.AppUser, DomainApp.Gift, DALAppDTO.Gift>,
        IGiftRepository
    {
        public GiftRepository(AppDbContext dbContext) :
            base(dbContext, new BaseMapper<DomainApp.Gift, DALAppDTO.Gift>())
        {
        }

        // public async Task<IEnumerable<Gift>> AllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(g => g.ActionType)
        //         .Include(g => g.Status)
        //         .Include(g => g.AppUser)
        //         .Include(g => g.Wishlist)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // See your own gifts
        //         query = query.Where(g => g.AppUserId == userId);
        //     }
        //
        //     return await query.ToListAsync();
        // }
        //
        // public async Task<Gift> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Where(g => g.Id == id)
        //         .Include(g => g.ActionType)
        //         .Include(g => g.Status)
        //         .Include(g => g.AppUser)
        //         .Include(g => g.Wishlist)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // See your own gifts
        //         query = query.Where(g => g.AppUserId == userId);
        //     }
        //     
        //     return await query.FirstOrDefaultAsync();
        // }
        //
        // public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        // {
        //     if (userId != null)
        //     {
        //         return await RepoDbSet.AnyAsync(g => g.Id == id && g.AppUserId == userId);
        //     }
        //     return await RepoDbSet.AnyAsync(g => g.Id == id);
        // }
        //
        // public async Task DeleteAsync(Guid id, Guid? userId = null)
        // {
        //     var gift = await FirstOrDefaultAsync(id, userId);
        //     base.Remove(gift);
        // }
        //
        // public async Task<IEnumerable<GiftDTO>> DTOAllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(g => g.ActionType)
        //         .Include(g => g.Status)
        //         .Include(g => g.AppUser)
        //         .Include(g => g.Wishlist)
        //         .AsQueryable();
        //     if (userId != null)
        //     {
        //         // See your own gifts
        //         query = query.Where(g => g.AppUserId == userId);
        //     }
        //
        //     return await query
        //         .Select(g => new GiftDTO()
        //             {
        //                 Id = g.Id,
        //                 Name = g.Name,
        //                 Description = g.Description,
        //                 Image = g.Image,
        //                 Url = g.Url,
        //                 PartnerUrl = g.PartnerUrl,
        //                 IsPartnered = g.IsPartnered,
        //                 IsPinned = g.IsPinned,
        //                 AppUserId = g.AppUserId,
        //                 ActionTypeId = g.ActionTypeId,
        //                 StatusId = g.StatusId,
        //                 WishlistId = g.WishlistId,
        //                 ArchivedGiftsCount = g.ArchivedGifts.Count,
        //                 ReservedGiftsCount = g.ReservedGifts.Count,
        //                 AppUser = new AppUserDTO()
        //                 {
        //                     Id = g.AppUser!.Id,
        //                     FirstName = g.AppUser!.FirstName,
        //                     LastName = g.AppUser!.LastName,
        //                     IsCampaignManager = g.AppUser!.IsCampaignManager,
        //                     IsActive = g.AppUser!.IsActive,
        //                     LastActive = g.AppUser!.LastActive,
        //                     DateJoined = g.AppUser!.DateJoined,
        //                     UserPermissionsCount = g.AppUser!.UserPermissions.Count,
        //                     UserProfilesCount = g.AppUser!.UserProfiles.Count,
        //                     UserNotificationsCount = g.AppUser!.UserNotifications.Count,
        //                     UserCampaignsCount = g.AppUser!.UserCampaigns.Count,
        //                     GiftsCount = g.AppUser!.Gifts.Count,
        //                     ReservedGiftsByUserCount = g.AppUser!.ReservedGiftsByUser.Count,
        //                     ReservedGiftsForUserCount = g.AppUser!.ReservedGiftsForUser.Count,
        //                     ArchivedGiftsByUserCount = g.AppUser!.ArchivedGiftsByUser.Count,
        //                     ArchivedGiftsForUserCount = g.AppUser!.ArchivedGiftsForUser.Count,
        //                     ConfirmedFriendshipsCount = g.AppUser!.ConfirmedFriendships.Count,
        //                     PendingFriendshipsCount = g.AppUser!.PendingFriendships.Count,
        //                     SentPrivateMessagesCount = g.AppUser!.SentPrivateMessages.Count,
        //                     ReceivedPrivateMessagesCount = g.AppUser!.ReceivedPrivateMessages.Count,
        //                     InvitedUsersCount = g.AppUser!.InvitedUsers.Count
        //                 },
        //                 ActionType = new ActionTypeDTO()
        //                 {
        //                     Id = g.ActionType!.Id,
        //                     Comment = g.ActionType!.Comment,
        //                     DonateesCount = g.ActionType!.Donatees.Count,
        //                     GiftsCount = g.ActionType!.Gifts.Count,
        //                     ActionTypeValue = g.ActionType!.ActionTypeValue,
        //                     ArchivedGiftsCount = g.ActionType!.ArchivedGifts.Count,
        //                     ReservedGiftsCount = g.ActionType!.ReservedGifts.Count,
        //                 },
        //                 Status = new StatusDTO()
        //                 {
        //                     Id = g.Status!.Id,
        //                     Comment = g.Status!.Comment,
        //                     DonateesCount = g.Status!.Donatees.Count,
        //                     GiftsCount = g.Status!.Gifts.Count,
        //                     StatusValue = g.Status!.StatusValue,
        //                     ArchivedGiftsCount = g.Status!.ArchivedGifts.Count,
        //                     ReservedGiftsCount = g.Status!.ReservedGifts.Count
        //                 },
        //                 Wishlist = new WishlistDTO()
        //                 {
        //                     Id = g.Wishlist!.Id,
        //                     Comment = g.Wishlist!.Comment,
        //                     GiftsCount = g.Wishlist!.Gifts.Count,
        //                     ProfilesCount = g.Wishlist!.Profiles.Count
        //                 }
        //             }).ToListAsync();
        // }
        //
        // public async Task<GiftDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Where(g => g.Id == id)
        //         .Include(g => g.ActionType)
        //         .Include(g => g.Status)
        //         .Include(g => g.AppUser)
        //         .Include(g => g.Wishlist)
        //         .AsQueryable();
        //     if (userId != null)
        //     {
        //         // See your own gifts
        //         query = query.Where(g => g.AppUserId == userId);
        //     }
        //     
        //     return await query.Select(g => new GiftDTO()
        //             {
        //                 Id = g.Id,
        //                 Name = g.Name,
        //                 Description = g.Description,
        //                 Image = g.Image,
        //                 Url = g.Url,
        //                 PartnerUrl = g.PartnerUrl,
        //                 IsPartnered = g.IsPartnered,
        //                 IsPinned = g.IsPinned,
        //                 AppUserId = g.AppUserId,
        //                 ActionTypeId = g.ActionTypeId,
        //                 StatusId = g.StatusId,
        //                 WishlistId = g.WishlistId,
        //                 ArchivedGiftsCount = g.ArchivedGifts.Count,
        //                 ReservedGiftsCount = g.ReservedGifts.Count,
        //                 AppUser = new AppUserDTO()
        //                 {
        //                     Id = g.AppUser!.Id,
        //                     FirstName = g.AppUser!.FirstName,
        //                     LastName = g.AppUser!.LastName,
        //                     IsCampaignManager = g.AppUser!.IsCampaignManager,
        //                     IsActive = g.AppUser!.IsActive,
        //                     LastActive = g.AppUser!.LastActive,
        //                     DateJoined = g.AppUser!.DateJoined,
        //                     UserPermissionsCount = g.AppUser!.UserPermissions.Count,
        //                     UserProfilesCount = g.AppUser!.UserProfiles.Count,
        //                     UserNotificationsCount = g.AppUser!.UserNotifications.Count,
        //                     UserCampaignsCount = g.AppUser!.UserCampaigns.Count,
        //                     GiftsCount = g.AppUser!.Gifts.Count,
        //                     ReservedGiftsByUserCount = g.AppUser!.ReservedGiftsByUser.Count,
        //                     ReservedGiftsForUserCount = g.AppUser!.ReservedGiftsForUser.Count,
        //                     ArchivedGiftsByUserCount = g.AppUser!.ArchivedGiftsByUser.Count,
        //                     ArchivedGiftsForUserCount = g.AppUser!.ArchivedGiftsForUser.Count,
        //                     ConfirmedFriendshipsCount = g.AppUser!.ConfirmedFriendships.Count,
        //                     PendingFriendshipsCount = g.AppUser!.PendingFriendships.Count,
        //                     SentPrivateMessagesCount = g.AppUser!.SentPrivateMessages.Count,
        //                     ReceivedPrivateMessagesCount = g.AppUser!.ReceivedPrivateMessages.Count,
        //                     InvitedUsersCount = g.AppUser!.InvitedUsers.Count
        //                 },
        //                 ActionType = new ActionTypeDTO()
        //                 {
        //                     Id = g.ActionType!.Id,
        //                     Comment = g.ActionType!.Comment,
        //                     DonateesCount = g.ActionType!.Donatees.Count,
        //                     GiftsCount = g.ActionType!.Gifts.Count,
        //                     ActionTypeValue = g.ActionType!.ActionTypeValue,
        //                     ArchivedGiftsCount = g.ActionType!.ArchivedGifts.Count,
        //                     ReservedGiftsCount = g.ActionType!.ReservedGifts.Count,
        //                 },
        //                 Status = new StatusDTO()
        //                 {
        //                     Id = g.Status!.Id,
        //                     Comment = g.Status!.Comment,
        //                     DonateesCount = g.Status!.Donatees.Count,
        //                     GiftsCount = g.Status!.Gifts.Count,
        //                     StatusValue = g.Status!.StatusValue,
        //                     ArchivedGiftsCount = g.Status!.ArchivedGifts.Count,
        //                     ReservedGiftsCount = g.Status!.ReservedGifts.Count
        //                 },
        //                 Wishlist = new WishlistDTO()
        //                 {
        //                     Id = g.Wishlist!.Id,
        //                     Comment = g.Wishlist!.Comment,
        //                     GiftsCount = g.Wishlist!.Gifts.Count,
        //                     ProfilesCount = g.Wishlist!.Profiles.Count
        //                 }
        //     }).FirstOrDefaultAsync();
        // }

        /*
        // Return only gifts that start with the letter "a" - random override example
        public override async Task<IEnumerable<Gift>> AllAsync()
        {
            return await RepoDbSet.Where(g => g.Name.StartsWith("a")).ToListAsync();
        }
        */
    }
}
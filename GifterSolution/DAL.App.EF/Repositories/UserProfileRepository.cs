using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using DomainApp = Domain.App;
using DALAppDTO = DAL.App.DTO;
using DomainAppIdentity = Domain.App.Identity;

namespace DAL.App.EF.Repositories
{
    public class UserProfileRepository :
        EFBaseRepository<AppDbContext, DomainAppIdentity.AppUser, DomainApp.UserProfile, DALAppDTO.UserProfileDAL>,
        IUserProfileRepository
    {
        public UserProfileRepository(AppDbContext dbContext) :
            base(dbContext, new DALMapper<DomainApp.UserProfile, DALAppDTO.UserProfileDAL>())
        {
        }

        // public async Task<IEnumerable<UserProfile>> AllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(up => up.AppUser)
        //         .Include(up => up.Profile)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // See your own profile
        //         query = query.Where(up => up.AppUserId == userId);
        //     }
        //
        //     return await query.ToListAsync();
        // }
        //
        // public async Task<UserProfile> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Where(up => up.Id == id)
        //         .Include(up => up.AppUser)
        //         .Include(up => up.Profile)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // See your own profile
        //         query = query.Where(up => up.AppUserId == userId);
        //     }
        //     
        //     return await query.FirstOrDefaultAsync();
        // }
        //
        // public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        // {
        //     if (userId != null)
        //     {
        //         return await RepoDbSet.AnyAsync(up => up.Id == id && up.AppUserId == userId);
        //     }
        //     return await RepoDbSet.AnyAsync(up => up.Id == id);
        // }
        //
        // public async Task DeleteAsync(Guid id, Guid? userId = null)
        // {
        //     var userPermission = await FirstOrDefaultAsync(id, userId);
        //     base.Remove(userPermission);
        // }
        //
        // public async Task<IEnumerable<UserProfileDTO>> DTOAllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(up => up.AppUser)
        //         .Include(up => up.Profile)
        //         .AsQueryable();
        //     if (userId != null)
        //     {
        //         // See your own profile
        //         query = query.Where(up => up.AppUserId == userId);
        //     }
        //
        //     return await query
        //         .Select(up => new UserProfileDTO()
        //         {
        //             Id = up.Id,
        //             Comment = up.Comment,
        //             AppUserId = up.AppUserId,
        //             ProfileId = up.ProfileId,
        //             AppUser = new AppUserDTO()
        //             {
        //                 Id = up.AppUser!.Id,
        //                 FirstName = up.AppUser!.FirstName,
        //                 LastName = up.AppUser!.LastName,
        //                 IsCampaignManager = up.AppUser!.IsCampaignManager,
        //                 IsActive = up.AppUser!.IsActive,
        //                 LastActive = up.AppUser!.LastActive,
        //                 DateJoined = up.AppUser!.DateJoined,
        //                 UserPermissionsCount = up.AppUser!.UserPermissions.Count,
        //                 UserProfilesCount = up.AppUser!.UserProfiles.Count,
        //                 UserCampaignsCount = up.AppUser!.UserCampaigns.Count,
        //                 UserNotificationsCount = up.AppUser!.UserNotifications.Count,
        //                 GiftsCount = up.AppUser!.Gifts.Count,
        //                 ReservedGiftsByUserCount = up.AppUser!.ReservedGiftsByUser.Count,
        //                 ReservedGiftsForUserCount = up.AppUser!.ReservedGiftsForUser.Count,
        //                 ArchivedGiftsByUserCount = up.AppUser!.ArchivedGiftsByUser.Count,
        //                 ArchivedGiftsForUserCount = up.AppUser!.ArchivedGiftsForUser.Count,
        //                 ConfirmedFriendshipsCount = up.AppUser!.ConfirmedFriendships.Count,
        //                 PendingFriendshipsCount = up.AppUser!.PendingFriendships.Count,
        //                 SentPrivateMessagesCount = up.AppUser!.SentPrivateMessages.Count,
        //                 ReceivedPrivateMessagesCount = up.AppUser!.ReceivedPrivateMessages.Count,
        //                 InvitedUsersCount = up.AppUser!.InvitedUsers.Count
        //             },
        //             Profile = new ProfileDTO()
        //             {
        //                 Id = up.Profile!.Id,
        //                 ProfilePicture = up.Profile!.ProfilePicture,
        //                 Gender = up.Profile!.Gender,
        //                 Bio = up.Profile!.Bio,
        //                 Age = up.Profile!.Age,
        //                 IsPrivate = up.Profile!.IsPrivate,
        //                 AppUserId = up.Profile!.AppUserId,
        //                 WishlistId = up.Profile!.WishlistId,
        //                 UserProfilesCount = up.Profile!.UserProfiles.Count,
        //                 AppUser = new AppUserDTO()
        //                 {
        //                     Id = up.Profile!.AppUser!.Id,
        //                     FirstName = up.Profile!.AppUser!.FirstName,
        //                     LastName = up.Profile!.AppUser!.LastName,
        //                     IsCampaignManager = up.Profile!.AppUser!.IsCampaignManager,
        //                     IsActive = up.Profile!.AppUser!.IsActive,
        //                     LastActive = up.Profile!.AppUser!.LastActive,
        //                     DateJoined = up.Profile!.AppUser!.DateJoined,
        //                     UserPermissionsCount = up.Profile!.AppUser!.UserPermissions.Count,
        //                     UserProfilesCount = up.Profile!.AppUser!.UserProfiles.Count,
        //                     UserCampaignsCount = up.Profile!.AppUser!.UserCampaigns.Count,
        //                     UserNotificationsCount = up.Profile!.AppUser!.UserNotifications.Count,
        //                     GiftsCount = up.Profile!.AppUser!.Gifts.Count,
        //                     ReservedGiftsByUserCount = up.Profile!.AppUser!.ReservedGiftsByUser.Count,
        //                     ReservedGiftsForUserCount = up.Profile!.AppUser!.ReservedGiftsForUser.Count,
        //                     ArchivedGiftsByUserCount = up.Profile!.AppUser!.ArchivedGiftsByUser.Count,
        //                     ArchivedGiftsForUserCount = up.Profile!.AppUser!.ArchivedGiftsForUser.Count,
        //                     ConfirmedFriendshipsCount = up.Profile!.AppUser!.ConfirmedFriendships.Count,
        //                     PendingFriendshipsCount = up.Profile!.AppUser!.PendingFriendships.Count,
        //                     SentPrivateMessagesCount = up.Profile!.AppUser!.SentPrivateMessages.Count,
        //                     ReceivedPrivateMessagesCount = up.Profile!.AppUser!.ReceivedPrivateMessages.Count,
        //                     InvitedUsersCount = up.Profile!.AppUser!.InvitedUsers.Count
        //                 },
        //                 Wishlist = new WishlistDTO()
        //                 {
        //                     Id = up.Profile!.Wishlist!.Id,
        //                     Comment = up.Profile!.Wishlist!.Comment,
        //                     GiftsCount = up.Profile!.Wishlist!.Gifts.Count,
        //                     ProfilesCount = up.Profile!.Wishlist!.Profiles.Count,
        //                 }
        //             }
        //         }).ToListAsync();
        // }
        //
        // public async Task<UserProfileDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Where(up => up.Id == id)
        //         .Include(up => up.AppUser)
        //         .Include(up => up.Profile)
        //         .AsQueryable();
        //     if (userId != null)
        //     {
        //         // See your own profile
        //         query = query.Where(up => up.AppUserId == userId);
        //     }
        //     
        //     return await query.Select(up => new UserProfileDTO()
        //     {
        //         Id = up.Id,
        //         Comment = up.Comment,
        //         AppUserId = up.AppUserId,
        //         ProfileId = up.ProfileId,
        //         AppUser = new AppUserDTO()
        //         {
        //             Id = up.AppUser!.Id,
        //             FirstName = up.AppUser!.FirstName,
        //             LastName = up.AppUser!.LastName,
        //             IsCampaignManager = up.AppUser!.IsCampaignManager,
        //             IsActive = up.AppUser!.IsActive,
        //             LastActive = up.AppUser!.LastActive,
        //             DateJoined = up.AppUser!.DateJoined,
        //             UserPermissionsCount = up.AppUser!.UserPermissions.Count,
        //             UserProfilesCount = up.AppUser!.UserProfiles.Count,
        //             UserCampaignsCount = up.AppUser!.UserCampaigns.Count,
        //             UserNotificationsCount = up.AppUser!.UserNotifications.Count,
        //             GiftsCount = up.AppUser!.Gifts.Count,
        //             ReservedGiftsByUserCount = up.AppUser!.ReservedGiftsByUser.Count,
        //             ReservedGiftsForUserCount = up.AppUser!.ReservedGiftsForUser.Count,
        //             ArchivedGiftsByUserCount = up.AppUser!.ArchivedGiftsByUser.Count,
        //             ArchivedGiftsForUserCount = up.AppUser!.ArchivedGiftsForUser.Count,
        //             ConfirmedFriendshipsCount = up.AppUser!.ConfirmedFriendships.Count,
        //             PendingFriendshipsCount = up.AppUser!.PendingFriendships.Count,
        //             SentPrivateMessagesCount = up.AppUser!.SentPrivateMessages.Count,
        //             ReceivedPrivateMessagesCount = up.AppUser!.ReceivedPrivateMessages.Count,
        //             InvitedUsersCount = up.AppUser!.InvitedUsers.Count
        //         },
        //         Profile = new ProfileDTO()
        //         {
        //             Id = up.Profile!.Id,
        //             ProfilePicture = up.Profile!.ProfilePicture,
        //             Gender = up.Profile!.Gender,
        //             Bio = up.Profile!.Bio,
        //             Age = up.Profile!.Age,
        //             IsPrivate = up.Profile!.IsPrivate,
        //             AppUserId = up.Profile!.AppUserId,
        //             WishlistId = up.Profile!.WishlistId,
        //             UserProfilesCount = up.Profile!.UserProfiles.Count,
        //             AppUser = new AppUserDTO()
        //             {
        //                 Id = up.Profile!.AppUser!.Id,
        //                 FirstName = up.Profile!.AppUser!.FirstName,
        //                 LastName = up.Profile!.AppUser!.LastName,
        //                 IsCampaignManager = up.Profile!.AppUser!.IsCampaignManager,
        //                 IsActive = up.Profile!.AppUser!.IsActive,
        //                 LastActive = up.Profile!.AppUser!.LastActive,
        //                 DateJoined = up.Profile!.AppUser!.DateJoined,
        //                 UserPermissionsCount = up.Profile!.AppUser!.UserPermissions.Count,
        //                 UserProfilesCount = up.Profile!.AppUser!.UserProfiles.Count,
        //                 UserCampaignsCount = up.Profile!.AppUser!.UserCampaigns.Count,
        //                 UserNotificationsCount = up.Profile!.AppUser!.UserNotifications.Count,
        //                 GiftsCount = up.Profile!.AppUser!.Gifts.Count,
        //                 ReservedGiftsByUserCount = up.Profile!.AppUser!.ReservedGiftsByUser.Count,
        //                 ReservedGiftsForUserCount = up.Profile!.AppUser!.ReservedGiftsForUser.Count,
        //                 ArchivedGiftsByUserCount = up.Profile!.AppUser!.ArchivedGiftsByUser.Count,
        //                 ArchivedGiftsForUserCount = up.Profile!.AppUser!.ArchivedGiftsForUser.Count,
        //                 ConfirmedFriendshipsCount = up.Profile!.AppUser!.ConfirmedFriendships.Count,
        //                 PendingFriendshipsCount = up.Profile!.AppUser!.PendingFriendships.Count,
        //                 SentPrivateMessagesCount = up.Profile!.AppUser!.SentPrivateMessages.Count,
        //                 ReceivedPrivateMessagesCount = up.Profile!.AppUser!.ReceivedPrivateMessages.Count,
        //                 InvitedUsersCount = up.Profile!.AppUser!.InvitedUsers.Count
        //             },
        //             Wishlist = new WishlistDTO()
        //             {
        //                 Id = up.Profile!.Wishlist!.Id,
        //                 Comment = up.Profile!.Wishlist!.Comment,
        //                 GiftsCount = up.Profile!.Wishlist!.Gifts.Count,
        //                 ProfilesCount = up.Profile!.Wishlist!.Profiles.Count,
        //             }
        //         }
        //     }).FirstOrDefaultAsync();
        // }
    }
}
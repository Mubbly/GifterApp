using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using com.mubbly.gifterapp.DAL.Base.EF.Repositories;
using com.mubbly.gifterapp.DAL.Base.Mappers;
using DomainApp = Domain.App;
using DALAppDTO = DAL.App.DTO;
using DomainAppIdentity = Domain.App.Identity;

namespace DAL.App.EF.Repositories
{
    public class UserCampaignRepository :
        EFBaseRepository<AppDbContext, DomainAppIdentity.AppUser, DomainApp.UserCampaign, DALAppDTO.UserCampaignDAL>,
        IUserCampaignRepository
    {
        public UserCampaignRepository(AppDbContext dbContext) :
            base(dbContext, new DALMapper<DomainApp.UserCampaign, DALAppDTO.UserCampaignDAL>())
        {
        }

        // public async Task<IEnumerable<UserCampaign>> AllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(uc => uc.AppUser)
        //         .Include(uc => uc.Campaign)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // See your own campaigns
        //         query = query.Where(uc => uc.AppUserId == userId);
        //     }
        //
        //     return await query.ToListAsync();
        // }
        //
        // public async Task<UserCampaign> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(uc => uc.AppUser)
        //         .Include(uc => uc.Campaign)
        //         .Where(uc => uc.Id == id)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // See your own campaigns
        //         query = query.Where(uc => uc.AppUserId == userId);
        //     }
        //     
        //     return await query.FirstOrDefaultAsync();
        // }
        //
        // public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        // {
        //     if (userId != null)
        //     {
        //         return await RepoDbSet.AnyAsync(uc => uc.Id == id && uc.AppUserId == userId);
        //     }
        //     return await RepoDbSet.AnyAsync(uc => uc.Id == id);
        // }
        //
        // public async Task DeleteAsync(Guid id, Guid? userId = null)
        // {
        //     var userCampaign = await FirstOrDefaultAsync(id, userId);
        //     base.Remove(userCampaign);
        // }
        //
        // public async Task<IEnumerable<UserCampaignDTO>> DTOAllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(uc => uc.AppUser)
        //         .Include(uc => uc.Campaign)
        //         .AsQueryable();
        //     if (userId != null)
        //     {
        //         // See your own campaigns
        //         query = query.Where(uc => uc.AppUserId == userId);
        //     }
        //
        //     return await query
        //         .Select(uc => new UserCampaignDTO()
        //         {
        //             Id = uc.Id,
        //             Comment = uc.Comment,
        //             AppUserId = uc.AppUserId,
        //             CampaignId = uc.CampaignId,
        //             AppUser = new AppUserDTO()
        //             {
        //                 Id = uc.AppUser!.Id,
        //                 FirstName = uc.AppUser!.FirstName,
        //                 LastName = uc.AppUser!.LastName,
        //                 IsCampaignManager = uc.AppUser!.IsCampaignManager,
        //                 IsActive = uc.AppUser!.IsActive,
        //                 LastActive = uc.AppUser!.LastActive,
        //                 DateJoined = uc.AppUser!.DateJoined,
        //                 UserPermissionsCount = uc.AppUser!.UserPermissions.Count,
        //                 UserProfilesCount = uc.AppUser!.UserProfiles.Count,
        //                 UserNotificationsCount = uc.AppUser!.UserNotifications.Count,
        //                 UserCampaignsCount = uc.AppUser!.UserCampaigns.Count,
        //                 GiftsCount = uc.AppUser!.Gifts.Count,
        //                 ReservedGiftsByUserCount = uc.AppUser!.ReservedGiftsByUser.Count,
        //                 ReservedGiftsForUserCount = uc.AppUser!.ReservedGiftsForUser.Count,
        //                 ArchivedGiftsByUserCount = uc.AppUser!.ArchivedGiftsByUser.Count,
        //                 ArchivedGiftsForUserCount = uc.AppUser!.ArchivedGiftsForUser.Count,
        //                 ConfirmedFriendshipsCount = uc.AppUser!.ConfirmedFriendships.Count,
        //                 PendingFriendshipsCount = uc.AppUser!.PendingFriendships.Count,
        //                 SentPrivateMessagesCount = uc.AppUser!.SentPrivateMessages.Count,
        //                 ReceivedPrivateMessagesCount = uc.AppUser!.ReceivedPrivateMessages.Count,
        //                 InvitedUsersCount = uc.AppUser!.InvitedUsers.Count
        //             },
        //             Campaign = new CampaignDTO()
        //             {
        //                 Id = uc.Campaign!.Id,
        //                 ActiveFromDate = uc.Campaign!.ActiveFromDate,
        //                 ActiveToDate = uc.Campaign!.ActiveToDate,
        //                 AdImage = uc.Campaign!.AdImage,
        //                 CampaignDonateesCount = uc.Campaign!.CampaignDonatees.Count,
        //                 Description = uc.Campaign!.Description,
        //                 Institution = uc.Campaign!.Institution,
        //                 IsActive = uc.Campaign!.IsActive,
        //                 Name = uc.Campaign!.Name,
        //                 UserCampaignsCount = uc.Campaign!.UserCampaigns.Count
        //             }
        //         }).ToListAsync();
        // }
        //
        // public async Task<UserCampaignDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Where(uc => uc.Id == id)
        //         .Include(uc => uc.AppUser)
        //         .Include(uc => uc.Campaign)
        //         .AsQueryable();
        //     if (userId != null)
        //     {
        //         // See your own campaigns
        //         query = query.Where(uc => uc.AppUserId == userId);
        //     }
        //     
        //     return await query.Select(uc => new UserCampaignDTO()
        //     {
        //         Id = uc.Id,
        //         Comment = uc.Comment,
        //         AppUserId = uc.AppUserId,
        //         CampaignId = uc.CampaignId,
        //         AppUser = new AppUserDTO()
        //         {
        //             Id = uc.AppUser!.Id,
        //             FirstName = uc.AppUser!.FirstName,
        //             LastName = uc.AppUser!.LastName,
        //             IsCampaignManager = uc.AppUser!.IsCampaignManager,
        //             IsActive = uc.AppUser!.IsActive,
        //             LastActive = uc.AppUser!.LastActive,
        //             DateJoined = uc.AppUser!.DateJoined,
        //             UserPermissionsCount = uc.AppUser!.UserPermissions.Count,
        //             UserProfilesCount = uc.AppUser!.UserProfiles.Count,
        //             UserNotificationsCount = uc.AppUser!.UserNotifications.Count,
        //             UserCampaignsCount = uc.AppUser!.UserCampaigns.Count,
        //             GiftsCount = uc.AppUser!.Gifts.Count,
        //             ReservedGiftsByUserCount = uc.AppUser!.ReservedGiftsByUser.Count,
        //             ReservedGiftsForUserCount = uc.AppUser!.ReservedGiftsForUser.Count,
        //             ArchivedGiftsByUserCount = uc.AppUser!.ArchivedGiftsByUser.Count,
        //             ArchivedGiftsForUserCount = uc.AppUser!.ArchivedGiftsForUser.Count,
        //             ConfirmedFriendshipsCount = uc.AppUser!.ConfirmedFriendships.Count,
        //             PendingFriendshipsCount = uc.AppUser!.PendingFriendships.Count,
        //             SentPrivateMessagesCount = uc.AppUser!.SentPrivateMessages.Count,
        //             ReceivedPrivateMessagesCount = uc.AppUser!.ReceivedPrivateMessages.Count,
        //             InvitedUsersCount = uc.AppUser!.InvitedUsers.Count
        //         },
        //         Campaign = new CampaignDTO()
        //         {
        //             Id = uc.Campaign!.Id,
        //             ActiveFromDate = uc.Campaign!.ActiveFromDate,
        //             ActiveToDate = uc.Campaign!.ActiveToDate,
        //             AdImage = uc.Campaign!.AdImage,
        //             CampaignDonateesCount = uc.Campaign!.CampaignDonatees.Count,
        //             Description = uc.Campaign!.Description,
        //             Institution = uc.Campaign!.Institution,
        //             IsActive = uc.Campaign!.IsActive,
        //             Name = uc.Campaign!.Name,
        //             UserCampaignsCount = uc.Campaign!.UserCampaigns.Count
        //         }
        //     }).FirstOrDefaultAsync();
        // }
    }
}
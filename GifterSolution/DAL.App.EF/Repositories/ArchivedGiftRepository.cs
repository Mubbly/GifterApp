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
    public class ArchivedGiftRepository : EFBaseRepository<ArchivedGift, AppDbContext>, IArchivedGiftRepository
    {
        public ArchivedGiftRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        // TODO: User stuff

        public async Task<IEnumerable<ArchivedGift>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Gift)
                .Include(a => a.ActionType)
                .Include(a => a.Status)
                .Include(a => a.UserGiver)
                .Include(a => a.UserReceiver)
                .AsQueryable();

            if (userId != null) 
            { 
                //query = query.Where(o => o.Campaign!.AppUserId == userId && o.Donatee!.AppUserId == userId);
            }

            return await query.ToListAsync();
        }

        public async Task<ArchivedGift> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Gift)
                .Include(a => a.ActionType)
                .Include(a => a.Status)
                .Include(a => a.UserGiver)
                .Include(a => a.UserReceiver)
                .Where(a => a.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                //query = query.Where(a => a.Campaign!.AppUserId == userId && a.Donatee!.AppUserId == userId);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId != null)
            {
                // return await RepoDbSet.AnyAsync(a =>
                //     a.Id == id && a.Campaign!.AppUserId == userId && a.Donatee!.AppUserId == userId);
            }
            
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var archivedGift = await FirstOrDefaultAsync(id); // (id, userId);
            base.Remove(archivedGift);
        }

        public async Task<IEnumerable<ArchivedGiftDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Gift)
                .Include(a => a.ActionType)
                .Include(a => a.Status)
                .Include(a => a.UserGiver)
                .Include(a => a.UserReceiver)
                .AsQueryable();
            
            if (userId != null)
            {
                //query = query.Where(o => o.Donatee!.AppUserId == userId && o.Campaign!.AppUserId == userId);
            }

            return await query
                .Select(ag => new ArchivedGiftDTO()
                {
                    Id = ag.Id,
                    Comment = ag.Comment,
                    DateArchived = ag.DateArchived,
                    IsConfirmed = ag.IsConfirmed,
                    GiftId = ag.GiftId,
                    ActionTypeId = ag.ActionTypeId,
                    StatusId = ag.StatusId,
                    UserGiverId = ag.UserGiverId,
                    UserReceiverId = ag.UserReceiverId,
                    Gift = new GiftDTO()
                    {
                        Id = ag.Gift!.Id,
                        Description = ag.Gift!.Description,
                        ActionTypeId = ag.Gift!.ActionTypeId,
                        AppUserId = ag.Gift!.AppUserId,
                        ArchivedGiftsCount = ag.Gift!.ArchivedGifts.Count,
                        Image = ag.Gift!.Image,
                        IsPartnered = ag.Gift!.IsPartnered,
                        IsPinned = ag.Gift!.IsPinned,
                        Name = ag.Gift!.Name,
                        PartnerUrl = ag.Gift!.PartnerUrl,
                        Url = ag.Gift!.Url,
                        StatusId = ag.Gift!.StatusId,
                        WishlistId = ag.Gift!.WishlistId,
                        ReservedGiftsCount = ag.Gift!.ReservedGifts.Count
                    },
                    ActionType = new ActionTypeDTO()
                    {
                        Id = ag.ActionType!.Id,
                        Comment = ag.ActionType!.Comment,
                        DonateesCount = ag.ActionType!.Donatees.Count,
                        GiftsCount = ag.ActionType!.Gifts.Count,
                        ActionTypeValue = ag.ActionType!.ActionTypeValue,
                        ArchivedGiftsCount = ag.ActionType!.ArchivedGifts.Count,
                        ReservedGiftsCount = ag.ActionType!.ReservedGifts.Count,
                    },
                    Status = new StatusDTO()
                    {
                        Id = ag.Status!.Id,
                        Comment = ag.Status!.Comment,
                        DonateesCount = ag.Status!.Donatees.Count,
                        GiftsCount = ag.Status!.Gifts.Count,
                        StatusValue = ag.Status!.StatusValue,
                        ArchivedGiftsCount = ag.Status!.ArchivedGifts.Count,
                        ReservedGiftsCount = ag.Status!.ReservedGifts.Count
                    },
                    UserGiver = new AppUserDTO()
                    {
                        Id = ag.UserGiver!.Id,
                        FirstName = ag.UserGiver!.FirstName,
                        LastName = ag.UserGiver!.LastName,
                        IsCampaignManager = ag.UserGiver!.IsCampaignManager,
                        IsActive = ag.UserGiver!.IsActive,
                        LastActive = ag.UserGiver!.LastActive,
                        DateJoined = ag.UserGiver!.DateJoined,
                        UserPermissionsCount = ag.UserGiver!.UserPermissions.Count,
                        UserProfilesCount = ag.UserGiver!.UserProfiles.Count,
                        UserNotificationsCount = ag.UserGiver!.UserNotifications.Count,
                        UserCampaignsCount = ag.UserGiver!.UserCampaigns.Count,
                        GiftsCount = ag.UserGiver!.Gifts.Count,
                        ReservedGiftsByUserCount = ag.UserGiver!.ReservedGiftsByUser.Count,
                        ReservedGiftsForUserCount = ag.UserGiver!.ReservedGiftsForUser.Count,
                        ArchivedGiftsByUserCount = ag.UserGiver!.ArchivedGiftsByUser.Count,
                        ArchivedGiftsForUserCount = ag.UserGiver!.ArchivedGiftsForUser.Count,
                        ConfirmedFriendshipsCount = ag.UserGiver!.ConfirmedFriendships.Count,
                        PendingFriendshipsCount = ag.UserGiver!.PendingFriendships.Count,
                        SentPrivateMessagesCount = ag.UserGiver!.SentPrivateMessages.Count,
                        ReceivedPrivateMessagesCount = ag.UserGiver!.ReceivedPrivateMessages.Count,
                        InvitedUsersCount = ag.UserGiver!.InvitedUsers.Count
                    },
                    UserReceiver = new AppUserDTO()
                    {
                        Id = ag.UserReceiver!.Id,
                        FirstName = ag.UserReceiver!.FirstName,
                        LastName = ag.UserReceiver!.LastName,
                        IsCampaignManager = ag.UserReceiver!.IsCampaignManager,
                        IsActive = ag.UserReceiver!.IsActive,
                        LastActive = ag.UserReceiver!.LastActive,
                        DateJoined = ag.UserReceiver!.DateJoined,
                        UserPermissionsCount = ag.UserReceiver!.UserPermissions.Count,
                        UserProfilesCount = ag.UserReceiver!.UserProfiles.Count,
                        UserNotificationsCount = ag.UserReceiver!.UserNotifications.Count,
                        UserCampaignsCount = ag.UserReceiver!.UserCampaigns.Count,
                        GiftsCount = ag.UserReceiver!.Gifts.Count,
                        ReservedGiftsByUserCount = ag.UserReceiver!.ReservedGiftsByUser.Count,
                        ReservedGiftsForUserCount = ag.UserReceiver!.ReservedGiftsForUser.Count,
                        ArchivedGiftsByUserCount = ag.UserReceiver!.ArchivedGiftsByUser.Count,
                        ArchivedGiftsForUserCount = ag.UserReceiver!.ArchivedGiftsForUser.Count,
                        ConfirmedFriendshipsCount = ag.UserReceiver!.ConfirmedFriendships.Count,
                        PendingFriendshipsCount = ag.UserReceiver!.PendingFriendships.Count,
                        SentPrivateMessagesCount = ag.UserReceiver!.SentPrivateMessages.Count,
                        ReceivedPrivateMessagesCount = ag.UserReceiver!.ReceivedPrivateMessages.Count,
                        InvitedUsersCount = ag.UserReceiver!.InvitedUsers.Count
                    }
                }).ToListAsync();
        }

        public async Task<ArchivedGiftDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(a => a.Gift)
                .Include(a => a.ActionType)
                .Include(a => a.Status)
                .Include(a => a.UserGiver)
                .Include(a => a.UserReceiver)
                .AsQueryable();

            if (userId != null)
            {
            }

            return await query.Select(ag => new ArchivedGiftDTO()
            {
                Id = ag.Id,
                Comment = ag.Comment,
                DateArchived = ag.DateArchived,
                IsConfirmed = ag.IsConfirmed,
                GiftId = ag.GiftId,
                ActionTypeId = ag.ActionTypeId,
                StatusId = ag.StatusId,
                UserGiverId = ag.UserGiverId,
                UserReceiverId = ag.UserReceiverId,
                Gift = new GiftDTO()
                    {
                        Id = ag.Gift!.Id,
                        Description = ag.Gift!.Description,
                        ActionTypeId = ag.Gift!.ActionTypeId,
                        AppUserId = ag.Gift!.AppUserId,
                        ArchivedGiftsCount = ag.Gift!.ArchivedGifts.Count,
                        Image = ag.Gift!.Image,
                        IsPartnered = ag.Gift!.IsPartnered,
                        IsPinned = ag.Gift!.IsPinned,
                        Name = ag.Gift!.Name,
                        PartnerUrl = ag.Gift!.PartnerUrl,
                        Url = ag.Gift!.Url,
                        StatusId = ag.Gift!.StatusId,
                        WishlistId = ag.Gift!.WishlistId,
                        ReservedGiftsCount = ag.Gift!.ReservedGifts.Count
                    },
                    ActionType = new ActionTypeDTO()
                    {
                        Id = ag.ActionType!.Id,
                        Comment = ag.ActionType!.Comment,
                        DonateesCount = ag.ActionType!.Donatees.Count,
                        GiftsCount = ag.ActionType!.Gifts.Count,
                        ActionTypeValue = ag.ActionType!.ActionTypeValue,
                        ArchivedGiftsCount = ag.ActionType!.ArchivedGifts.Count,
                        ReservedGiftsCount = ag.ActionType!.ReservedGifts.Count,
                    },
                    Status = new StatusDTO()
                    {
                        Id = ag.Status!.Id,
                        Comment = ag.Status!.Comment,
                        DonateesCount = ag.Status!.Donatees.Count,
                        GiftsCount = ag.Status!.Gifts.Count,
                        StatusValue = ag.Status!.StatusValue,
                        ArchivedGiftsCount = ag.Status!.ArchivedGifts.Count,
                        ReservedGiftsCount = ag.Status!.ReservedGifts.Count
                    },
                    UserGiver = new AppUserDTO()
                    {
                        Id = ag.UserGiver!.Id,
                        FirstName = ag.UserGiver!.FirstName,
                        LastName = ag.UserGiver!.LastName,
                        IsCampaignManager = ag.UserGiver!.IsCampaignManager,
                        IsActive = ag.UserGiver!.IsActive,
                        LastActive = ag.UserGiver!.LastActive,
                        DateJoined = ag.UserGiver!.DateJoined,
                        UserPermissionsCount = ag.UserGiver!.UserPermissions.Count,
                        UserProfilesCount = ag.UserGiver!.UserProfiles.Count,
                        UserNotificationsCount = ag.UserGiver!.UserNotifications.Count,
                        UserCampaignsCount = ag.UserGiver!.UserCampaigns.Count,
                        GiftsCount = ag.UserGiver!.Gifts.Count,
                        ReservedGiftsByUserCount = ag.UserGiver!.ReservedGiftsByUser.Count,
                        ReservedGiftsForUserCount = ag.UserGiver!.ReservedGiftsForUser.Count,
                        ArchivedGiftsByUserCount = ag.UserGiver!.ArchivedGiftsByUser.Count,
                        ArchivedGiftsForUserCount = ag.UserGiver!.ArchivedGiftsForUser.Count,
                        ConfirmedFriendshipsCount = ag.UserGiver!.ConfirmedFriendships.Count,
                        PendingFriendshipsCount = ag.UserGiver!.PendingFriendships.Count,
                        SentPrivateMessagesCount = ag.UserGiver!.SentPrivateMessages.Count,
                        ReceivedPrivateMessagesCount = ag.UserGiver!.ReceivedPrivateMessages.Count,
                        InvitedUsersCount = ag.UserGiver!.InvitedUsers.Count
                    },
                    UserReceiver = new AppUserDTO()
                    {
                        Id = ag.UserReceiver!.Id,
                        FirstName = ag.UserReceiver!.FirstName,
                        LastName = ag.UserReceiver!.LastName,
                        IsCampaignManager = ag.UserReceiver!.IsCampaignManager,
                        IsActive = ag.UserReceiver!.IsActive,
                        LastActive = ag.UserReceiver!.LastActive,
                        DateJoined = ag.UserReceiver!.DateJoined,
                        UserPermissionsCount = ag.UserReceiver!.UserPermissions.Count,
                        UserProfilesCount = ag.UserReceiver!.UserProfiles.Count,
                        UserNotificationsCount = ag.UserReceiver!.UserNotifications.Count,
                        UserCampaignsCount = ag.UserReceiver!.UserCampaigns.Count,
                        GiftsCount = ag.UserReceiver!.Gifts.Count,
                        ReservedGiftsByUserCount = ag.UserReceiver!.ReservedGiftsByUser.Count,
                        ReservedGiftsForUserCount = ag.UserReceiver!.ReservedGiftsForUser.Count,
                        ArchivedGiftsByUserCount = ag.UserReceiver!.ArchivedGiftsByUser.Count,
                        ArchivedGiftsForUserCount = ag.UserReceiver!.ArchivedGiftsForUser.Count,
                        ConfirmedFriendshipsCount = ag.UserReceiver!.ConfirmedFriendships.Count,
                        PendingFriendshipsCount = ag.UserReceiver!.PendingFriendships.Count,
                        SentPrivateMessagesCount = ag.UserReceiver!.SentPrivateMessages.Count,
                        ReceivedPrivateMessagesCount = ag.UserReceiver!.ReceivedPrivateMessages.Count,
                        InvitedUsersCount = ag.UserReceiver!.InvitedUsers.Count
                    },
            }).FirstOrDefaultAsync();
        }
        
        // public async Task<IEnumerable<ArchivedGift>> AllAsyncWithConnectedData()
        // {
        //     var archivedGifts = RepoDbSet
        //         .Include(a => a.ActionType)
        //         .Include(a => a.Gift)
        //         .Include(a => a.Status)
        //         .Include(a => a.UserGiver)
        //         .Include(a => a.UserReceiver);
        //     return await archivedGifts.ToListAsync();
        // }
        //
        // public async Task<ArchivedGift> FindAsyncWithConnectedData(params object[] id)
        // {
        //     var archivedGift = await base.FindAsync(id);
        //     if (archivedGift.Equals(null))
        //     {
        //         return archivedGift;
        //     }
        //     // NB: Every load causes additional 'round trip' to the database... TODO: Is there a better way to do this?
        //     // TODO: Can't use lamba as param in .Reference() because entity name is not recognized - why?
        //     await RepoDbContext.Entry(archivedGift).Reference("ActionType").LoadAsync();
        //     await RepoDbContext.Entry(archivedGift).Reference("Gift").LoadAsync();
        //     await RepoDbContext.Entry(archivedGift).Reference("Status").LoadAsync();
        //     await RepoDbContext.Entry(archivedGift).Reference("UserGiver").LoadAsync();
        //     await RepoDbContext.Entry(archivedGift).Reference("UserReceiver").LoadAsync();
        //     return archivedGift;
        // }
        //
        // public Task<IEnumerable<ActionType>> GetArchivedGiftActionType()
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public Task<IEnumerable<Gift>> GetArchivedGiftGift()
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public Task<IEnumerable<Status>> GetArchivedGiftStatus()
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public Task<IEnumerable<AppUser>> GetArchivedGiftGiverUser()
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public Task<IEnumerable<AppUser>> GetArchivedGiftReceiverUser()
        // {
        //     throw new NotImplementedException();
        // }
    }
}
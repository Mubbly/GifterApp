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
    public class GiftRepository : EFBaseRepository<Gift, AppDbContext>, IGiftRepository
    {
        public GiftRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Gift>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(g => g.ActionType)
                .Include(g => g.Status)
                .Include(g => g.AppUser)
                .AsQueryable();

            if (userId != null)
            {
                //query = query.Where(o => o.Owner!.AppUserId == userId && o.Animal!.AppUserId == userId);
            }

            return await query.ToListAsync();
        }

        public async Task<Gift> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(g => g.Id == id)
                .Include(g => g.ActionType)
                .Include(g => g.Status)
                .Include(g => g.AppUser)
                .AsQueryable();

            if (userId != null)
            {
                //query = query.Where(a => a.Owner!.AppUserId == userId && a.Animal!.AppUserId == userId);
            }
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId != null)
            {
                // return await RepoDbSet.AnyAsync(a =>
                //     a.Id == id && a.Owner!.AppUserId == userId && a.Animal!.AppUserId == userId);
            }
            return await RepoDbSet.AnyAsync(g => g.Id == id);
        }

        public Task DeleteAsync(Guid id, Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GiftDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(g => g.ActionType)
                .Include(g => g.Status)
                .Include(g => g.AppUser)
                .AsQueryable();
            
            if (userId != null)
            {
                //query = query.Where(o => o.Donatee!.AppUserId == userId && o.Campaign!.AppUserId == userId);
            }

            return await query
                .Select(g => new GiftDTO()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        Description = g.Description,
                        Image = g.Image,
                        Url = g.Url,
                        PartnerUrl = g.PartnerUrl,
                        IsPartnered = g.IsPartnered,
                        IsPinned = g.IsPinned,
                        AppUserId = g.AppUserId,
                        ActionTypeId = g.ActionTypeId,
                        StatusId = g.StatusId,
                        ArchivedGiftsCount = g.ArchivedGifts.Count,
                        ReservedGiftsCount = g.ReservedGifts.Count,
                        WishlistsCount = g.Wishlists.Count,
                        AppUser = new AppUsersDTO()
                        {
                            Id = g.AppUser!.Id,
                            FirstName = g.AppUser!.FirstName,
                            LastName = g.AppUser!.LastName,
                            IsCampaignManager = g.AppUser!.IsCampaignManager,
                            IsActive = g.AppUser!.IsActive,
                            LastActive = g.AppUser!.LastActive,
                            DateJoined = g.AppUser!.DateJoined,
                            UserPermissionsCount = g.AppUser!.UserPermissions.Count,
                            UserProfilesCount = g.AppUser!.UserProfiles.Count,
                            UserNotificationsCount = g.AppUser!.UserNotifications.Count,
                            UserCampaignsCount = g.AppUser!.UserCampaigns.Count,
                            GiftsCount = g.AppUser!.Gifts.Count,
                            ReservedGiftsByUserCount = g.AppUser!.ReservedGiftsByUser.Count,
                            ReservedGiftsForUserCount = g.AppUser!.ReservedGiftsForUser.Count,
                            ArchivedGiftsByUserCount = g.AppUser!.ArchivedGiftsByUser.Count,
                            ArchivedGiftsForUserCount = g.AppUser!.ArchivedGiftsForUser.Count,
                            ConfirmedFriendshipsCount = g.AppUser!.ConfirmedFriendships.Count,
                            PendingFriendshipsCount = g.AppUser!.PendingFriendships.Count,
                            SentPrivateMessagesCount = g.AppUser!.SentPrivateMessages.Count,
                            ReceivedPrivateMessagesCount = g.AppUser!.ReceivedPrivateMessages.Count,
                            InvitedUsersCount = g.AppUser!.InvitedUsers.Count
                        },
                        ActionType = new ActionTypeDTO()
                        {
                            Id = g.ActionType!.Id,
                            Comment = g.ActionType!.Comment,
                            DonateesCount = g.ActionType!.Donatees.Count,
                            GiftsCount = g.ActionType!.Gifts.Count,
                            ActionTypeValue = g.ActionType!.ActionTypeValue,
                            ArchivedGiftsCount = g.ActionType!.ArchivedGifts.Count,
                            ReservedGiftsCount = g.ActionType!.ReservedGifts.Count,
                        },
                        Status = new StatusDTO()
                        {
                            Id = g.Status!.Id,
                            Comment = g.Status!.Comment,
                            DonateesCount = g.Status!.Donatees.Count,
                            GiftsCount = g.Status!.Gifts.Count,
                            StatusValue = g.Status!.StatusValue,
                            ArchivedGiftsCount = g.Status!.ArchivedGifts.Count,
                            ReservedGiftsCount = g.Status!.ReservedGifts.Count
                        }
                    }).ToListAsync();
        }

        public async Task<GiftDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(g => g.Id == id)
                .Include(g => g.ActionType)
                .Include(g => g.Status)
                .Include(g => g.AppUser)
                .AsQueryable();

            return await query.Select(g => new GiftDTO()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        Description = g.Description,
                        Image = g.Image,
                        Url = g.Url,
                        PartnerUrl = g.PartnerUrl,
                        IsPartnered = g.IsPartnered,
                        IsPinned = g.IsPinned,
                        AppUserId = g.AppUserId,
                        ActionTypeId = g.ActionTypeId,
                        StatusId = g.StatusId,
                        ArchivedGiftsCount = g.ArchivedGifts.Count,
                        ReservedGiftsCount = g.ReservedGifts.Count,
                        WishlistsCount = g.Wishlists.Count,
                        AppUser = new AppUsersDTO()
                        {
                            Id = g.AppUser!.Id,
                            FirstName = g.AppUser!.FirstName,
                            LastName = g.AppUser!.LastName,
                            IsCampaignManager = g.AppUser!.IsCampaignManager,
                            IsActive = g.AppUser!.IsActive,
                            LastActive = g.AppUser!.LastActive,
                            DateJoined = g.AppUser!.DateJoined,
                            UserPermissionsCount = g.AppUser!.UserPermissions.Count,
                            UserProfilesCount = g.AppUser!.UserProfiles.Count,
                            UserNotificationsCount = g.AppUser!.UserNotifications.Count,
                            UserCampaignsCount = g.AppUser!.UserCampaigns.Count,
                            GiftsCount = g.AppUser!.Gifts.Count,
                            ReservedGiftsByUserCount = g.AppUser!.ReservedGiftsByUser.Count,
                            ReservedGiftsForUserCount = g.AppUser!.ReservedGiftsForUser.Count,
                            ArchivedGiftsByUserCount = g.AppUser!.ArchivedGiftsByUser.Count,
                            ArchivedGiftsForUserCount = g.AppUser!.ArchivedGiftsForUser.Count,
                            ConfirmedFriendshipsCount = g.AppUser!.ConfirmedFriendships.Count,
                            PendingFriendshipsCount = g.AppUser!.PendingFriendships.Count,
                            SentPrivateMessagesCount = g.AppUser!.SentPrivateMessages.Count,
                            ReceivedPrivateMessagesCount = g.AppUser!.ReceivedPrivateMessages.Count,
                            InvitedUsersCount = g.AppUser!.InvitedUsers.Count
                        },
                        ActionType = new ActionTypeDTO()
                        {
                            Id = g.ActionType!.Id,
                            Comment = g.ActionType!.Comment,
                            DonateesCount = g.ActionType!.Donatees.Count,
                            GiftsCount = g.ActionType!.Gifts.Count,
                            ActionTypeValue = g.ActionType!.ActionTypeValue,
                            ArchivedGiftsCount = g.ActionType!.ArchivedGifts.Count,
                            ReservedGiftsCount = g.ActionType!.ReservedGifts.Count,
                        },
                        Status = new StatusDTO()
                        {
                            Id = g.Status!.Id,
                            Comment = g.Status!.Comment,
                            DonateesCount = g.Status!.Donatees.Count,
                            GiftsCount = g.Status!.Gifts.Count,
                            StatusValue = g.Status!.StatusValue,
                            ArchivedGiftsCount = g.Status!.ArchivedGifts.Count,
                            ReservedGiftsCount = g.Status!.ReservedGifts.Count
                        }
            }).FirstOrDefaultAsync();
        }

        /*
        // Return only gifts that start with the letter "a" - random override example
        public override async Task<IEnumerable<Gift>> AllAsync()
        {
            return await RepoDbSet.Where(g => g.Name.StartsWith("a")).ToListAsync();
        }
        */
        
    }
}
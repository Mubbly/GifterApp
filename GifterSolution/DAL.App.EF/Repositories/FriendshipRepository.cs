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
    public class FriendshipRepository :
        EFBaseRepository<AppDbContext, DomainAppIdentity.AppUser, DomainApp.Friendship, DALAppDTO.FriendshipDAL>,
        IFriendshipRepository
    {
        public FriendshipRepository(AppDbContext dbContext) :
            base(dbContext, new DALMapper<DomainApp.Friendship, DALAppDTO.FriendshipDAL>())
        {
        }
        
        public async Task<IEnumerable<DALAppDTO.FriendshipDAL>> GetAllForUserAsync(Guid userId, bool isConfirmed = true, bool noTracking = true)
        {
            // User's friendships
            var friendships = PrepareQuery(userId, noTracking);
            var personalFriendships = 
                await friendships
                    .Where(e => e.IsConfirmed == isConfirmed && (e.AppUser1Id == userId || e.AppUser2Id == userId))
                    .OrderBy(e => e.CreatedAt)
                    .Select(e => Mapper.Map(e))
                    .ToListAsync();
            return personalFriendships;
        }

        public async Task<DALAppDTO.FriendshipDAL> GetForUserAsync(Guid userId, Guid friendId, bool isConfirmed = true, bool noTracking = true)
        {
            var friendships = PrepareQuery(userId, noTracking);
            var personalFriendships =
                await friendships.FirstOrDefaultAsync(f => f.AppUser1Id == userId && f.AppUser2Id == friendId);
            return Mapper.Map(personalFriendships);
        }

        // public async Task<IEnumerable<Friendship>> AllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(f => f.AppUser1)
        //         .Include(f => f.AppUser2)
        //         .AsQueryable();
        //     if (userId != null)
        //     {
        //         // See only your friends
        //         query = query.Where(g => g.AppUser1Id == userId);
        //     }
        //     return await query.ToListAsync();
        // }
        //
        // public async Task<Friendship> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(f => f.AppUser1)
        //         .Include(f => f.AppUser2)
        //         .Where(f => f.Id == id)
        //         .AsQueryable();
        //     if (userId != null)
        //     {
        //         // See only your friends
        //         query = query.Where(f => f.AppUser1Id == userId);
        //     }
        //     return await query.FirstOrDefaultAsync();
        // }
        //
        // public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        // {
        //     if (userId != null)
        //     {
        //         return await RepoDbSet.AnyAsync(f => f.Id == id && f.AppUser1Id == userId);
        //     }
        //     return await RepoDbSet.AnyAsync(f => f.Id == id);
        // }
        //
        // public async Task DeleteAsync(Guid id, Guid? userId = null)
        // {
        //     var friendship = await FirstOrDefaultAsync(id, userId);
        //     base.Remove(friendship);
        // }
        //
        // public async Task<IEnumerable<FriendshipDTO>> DTOAllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(f => f.AppUser1)
        //         .Include(f => f.AppUser2)
        //         .AsQueryable();
        //     if (userId != null)
        //     {
        //         // See only your friends
        //         query = query.Where(f => f.AppUser1Id == userId);
        //     }
        //
        //     return await query
        //         .Select(f => new FriendshipDTO()
        //         {
        //             Id = f.Id,
        //             Comment = f.Comment,
        //             IsConfirmed = f.IsConfirmed,
        //             AppUser1Id = f.AppUser1Id,
        //             AppUser2Id = f.AppUser2Id,
        //             AppUser1 = new AppUserDTO
        //             {
        //                 Id = f.AppUser1!.Id,
        //                 FirstName = f.AppUser1!.FirstName,
        //                 LastName = f.AppUser1!.LastName,
        //                 IsFriendshipManager = f.AppUser1!.IsFriendshipManager,
        //                 IsActive = f.AppUser1!.IsActive,
        //                 LastActive = f.AppUser1!.LastActive,
        //                 DateJoined = f.AppUser1!.DateJoined,
        //                 UserPermissionsCount = f.AppUser1!.UserPermissions.Count,
        //                 UserProfilesCount = f.AppUser1!.UserProfiles.Count,
        //                 UserNotificationsCount = f.AppUser1!.UserNotifications.Count,
        //                 UserFriendshipsCount = f.AppUser1!.UserFriendships.Count,
        //                 FriendshipsCount = f.AppUser1!.Friendships.Count,
        //                 ReservedFriendshipsByUserCount = f.AppUser1!.ReservedFriendshipsByUser.Count,
        //                 ReservedFriendshipsForUserCount = f.AppUser1!.ReservedFriendshipsForUser.Count,
        //                 ArchivedFriendshipsByUserCount = f.AppUser1!.ArchivedFriendshipsByUser.Count,
        //                 ArchivedFriendshipsForUserCount = f.AppUser1!.ArchivedFriendshipsForUser.Count,
        //                 ConfirmedFriendshipsCount = f.AppUser1!.ConfirmedFriendships.Count,
        //                 PendingFriendshipsCount = f.AppUser1!.PendingFriendships.Count,
        //                 SentPrivateMessagesCount = f.AppUser1!.SentPrivateMessages.Count,
        //                 ReceivedPrivateMessagesCount = f.AppUser1!.ReceivedPrivateMessages.Count,
        //                 InvitedUsersCount = f.AppUser1!.InvitedUsers.Count
        //             },
        //             AppUser2 = new AppUserDTO
        //             {
        //                 Id = f.AppUser2!.Id,
        //                 FirstName = f.AppUser2!.FirstName,
        //                 LastName = f.AppUser2!.LastName,
        //                 IsFriendshipManager = f.AppUser2!.IsFriendshipManager,
        //                 IsActive = f.AppUser2!.IsActive,
        //                 LastActive = f.AppUser2!.LastActive,
        //                 DateJoined = f.AppUser2!.DateJoined,
        //                 UserPermissionsCount = f.AppUser2!.UserPermissions.Count,
        //                 UserProfilesCount = f.AppUser2!.UserProfiles.Count,
        //                 UserNotificationsCount = f.AppUser2!.UserNotifications.Count,
        //                 UserFriendshipsCount = f.AppUser2!.UserFriendships.Count,
        //                 FriendshipsCount = f.AppUser2!.Friendships.Count,
        //                 ReservedFriendshipsByUserCount = f.AppUser2!.ReservedFriendshipsByUser.Count,
        //                 ReservedFriendshipsForUserCount = f.AppUser2!.ReservedFriendshipsForUser.Count,
        //                 ArchivedFriendshipsByUserCount = f.AppUser2!.ArchivedFriendshipsByUser.Count,
        //                 ArchivedFriendshipsForUserCount = f.AppUser2!.ArchivedFriendshipsForUser.Count,
        //                 ConfirmedFriendshipsCount = f.AppUser2!.ConfirmedFriendships.Count,
        //                 PendingFriendshipsCount = f.AppUser2!.PendingFriendships.Count,
        //                 SentPrivateMessagesCount = f.AppUser2!.SentPrivateMessages.Count,
        //                 ReceivedPrivateMessagesCount = f.AppUser2!.ReceivedPrivateMessages.Count,
        //                 InvitedUsersCount = f.AppUser2!.InvitedUsers.Count
        //             }
        //         }).ToListAsync();
        // }
        //
        // public async Task<FriendshipDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(f => f.AppUser1)
        //         .Include(f => f.AppUser2)
        //         .Where(f => f.Id == id)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // See only your friends
        //         query = query.Where(f => f.AppUser1Id == userId);
        //     }
        //     
        //     return await query
        //         .Select(f => new FriendshipDTO()
        //         {
        //             Id = f.Id,
        //             Comment = f.Comment,
        //             IsConfirmed = f.IsConfirmed,
        //             AppUser1Id = f.AppUser1Id,
        //             AppUser2Id = f.AppUser2Id,
        //             AppUser1 = new AppUserDTO
        //             {
        //                 Id = f.AppUser1!.Id,
        //                 FirstName = f.AppUser1!.FirstName,
        //                 LastName = f.AppUser1!.LastName,
        //                 IsFriendshipManager = f.AppUser1!.IsFriendshipManager,
        //                 IsActive = f.AppUser1!.IsActive,
        //                 LastActive = f.AppUser1!.LastActive,
        //                 DateJoined = f.AppUser1!.DateJoined,
        //                 UserPermissionsCount = f.AppUser1!.UserPermissions.Count,
        //                 UserProfilesCount = f.AppUser1!.UserProfiles.Count,
        //                 UserNotificationsCount = f.AppUser1!.UserNotifications.Count,
        //                 UserFriendshipsCount = f.AppUser1!.UserFriendships.Count,
        //                 FriendshipsCount = f.AppUser1!.Friendships.Count,
        //                 ReservedFriendshipsByUserCount = f.AppUser1!.ReservedFriendshipsByUser.Count,
        //                 ReservedFriendshipsForUserCount = f.AppUser1!.ReservedFriendshipsForUser.Count,
        //                 ArchivedFriendshipsByUserCount = f.AppUser1!.ArchivedFriendshipsByUser.Count,
        //                 ArchivedFriendshipsForUserCount = f.AppUser1!.ArchivedFriendshipsForUser.Count,
        //                 ConfirmedFriendshipsCount = f.AppUser1!.ConfirmedFriendships.Count,
        //                 PendingFriendshipsCount = f.AppUser1!.PendingFriendships.Count,
        //                 SentPrivateMessagesCount = f.AppUser1!.SentPrivateMessages.Count,
        //                 ReceivedPrivateMessagesCount = f.AppUser1!.ReceivedPrivateMessages.Count,
        //                 InvitedUsersCount = f.AppUser1!.InvitedUsers.Count
        //             },
        //             AppUser2 = new AppUserDTO
        //             {
        //                 Id = f.AppUser2!.Id,
        //                 FirstName = f.AppUser2!.FirstName,
        //                 LastName = f.AppUser2!.LastName,
        //                 IsFriendshipManager = f.AppUser2!.IsFriendshipManager,
        //                 IsActive = f.AppUser2!.IsActive,
        //                 LastActive = f.AppUser2!.LastActive,
        //                 DateJoined = f.AppUser2!.DateJoined,
        //                 UserPermissionsCount = f.AppUser2!.UserPermissions.Count,
        //                 UserProfilesCount = f.AppUser2!.UserProfiles.Count,
        //                 UserNotificationsCount = f.AppUser2!.UserNotifications.Count,
        //                 UserFriendshipsCount = f.AppUser2!.UserFriendships.Count,
        //                 FriendshipsCount = f.AppUser2!.Friendships.Count,
        //                 ReservedFriendshipsByUserCount = f.AppUser2!.ReservedFriendshipsByUser.Count,
        //                 ReservedFriendshipsForUserCount = f.AppUser2!.ReservedFriendshipsForUser.Count,
        //                 ArchivedFriendshipsByUserCount = f.AppUser2!.ArchivedFriendshipsByUser.Count,
        //                 ArchivedFriendshipsForUserCount = f.AppUser2!.ArchivedFriendshipsForUser.Count,
        //                 ConfirmedFriendshipsCount = f.AppUser2!.ConfirmedFriendships.Count,
        //                 PendingFriendshipsCount = f.AppUser2!.PendingFriendships.Count,
        //                 SentPrivateMessagesCount = f.AppUser2!.SentPrivateMessages.Count,
        //                 ReceivedPrivateMessagesCount = f.AppUser2!.ReceivedPrivateMessages.Count,
        //                 InvitedUsersCount = f.AppUser2!.InvitedUsers.Count
        //             }
        //         }).FirstOrDefaultAsync();
        // }
    }
}
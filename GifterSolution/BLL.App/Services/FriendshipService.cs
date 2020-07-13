using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using com.mubbly.gifterapp.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;
#pragma warning disable 8603

namespace BLL.App.Services
{
    public class FriendshipService : BaseEntityService<IAppUnitOfWork,
            IFriendshipRepository, IFriendshipServiceMapper, DALAppDTO.FriendshipDAL, BLLAppDTO.FriendshipBLL>,
        IFriendshipService
    {
        public FriendshipService(IAppUnitOfWork uow) : base(uow, uow.Friendships, new FriendshipServiceMapper())
        {
        }

        public async Task<IEnumerable<BLLAppDTO.FriendshipResponseBLL>> GetAllForUserAsync(Guid userId,
            bool noTracking = true)
        {
            var confirmedFriendships = await UOW.Friendships.GetAllForUserAsync(userId, true);
            var pendingFriendships = await UOW.Friendships.GetAllForUserAsync(userId, false);
            
            // Join two lists, treat null as empty list
            var allFriendships = (confirmedFriendships ?? Enumerable.Empty<DALAppDTO.FriendshipDAL>())
                .Concat(pendingFriendships ?? Enumerable.Empty<DALAppDTO.FriendshipDAL>());

            return allFriendships.Select(friendship => GetFriendResponseData(friendship, userId));
        }

        public async Task<IEnumerable<BLLAppDTO.FriendshipResponseBLL>> GetAllConfirmedForUserAsync(Guid userId, bool noTracking = true)
        {
            var userFriendships = await UOW.Friendships.GetAllForUserAsync(userId, true, noTracking);
            return userFriendships.Select(friendship => GetFriendResponseData(friendship, userId));
        }

        /** Get either all sent requests or all received requests, based on isSent parameter */
        public async Task<IEnumerable<BLLAppDTO.FriendshipResponseBLL>> GetAllPendingForUserAsync(Guid userId, bool isSent = true, bool noTracking = true)
        {
            var sentFriendships = await UOW.Friendships.GetAllPendingForUserAsync(userId, isSent, noTracking);
            return sentFriendships.Select(friendship => GetFriendResponseData(friendship, userId)); 
        }

        /** Get a friendship regardless of its status (confirmed or pending) */
        public async Task<BLLAppDTO.FriendshipResponseBLL> GetForUserAsync(Guid userId, Guid friendId,
            bool noTracking = true)
        {
            var friendship = await UOW.Friendships.GetForUserAsync(userId, friendId, true, noTracking) ?? await UOW.Friendships.GetForUserAsync(userId, friendId, false, noTracking);
            return GetFriendResponseData(friendship, userId);
        }

        public async Task<BLLAppDTO.FriendshipResponseBLL> GetForUserConfirmedAsync(Guid userId, Guid friendId, bool noTracking = true)
        {
            var friendship = await UOW.Friendships.GetForUserAsync(userId, friendId, true, noTracking);
            return GetFriendResponseData(friendship, userId);
        }

        public async Task<BLLAppDTO.FriendshipResponseBLL> GetForUserPendingAsync(Guid userId, Guid friendId,
            bool noTracking = true)
        {
            var friendship = await UOW.Friendships.GetForUserAsync(userId, friendId, false, noTracking);
            return GetFriendResponseData(friendship, userId);
        }

        /**
         * Converts FriendshipDAL to FriendshipResponseBLL
         * Includes Name & LastActive values, fetched from Profile through AppUser
         */
        private BLLAppDTO.FriendshipResponseBLL GetFriendResponseData(DALAppDTO.FriendshipDAL friendship, Guid userId)
        {
            var friend = Mapper.MapFriendshipToResponseBLL(friendship);
            if (friend != null)
            {
                // Get friend's profile
                var friendId = userId == friend.AppUser2Id ? friend.AppUser1Id : friend.AppUser2Id;
                var friendProfile = UOW.Profiles.GetFullByUserAsync(friendId).Result;
                
                if (friendProfile != null)
                {
                    friend.Name = friendProfile.AppUser.FullName;
                    friend.Email = friendProfile.AppUser.Email;
                    friend.LastActive = friendProfile.AppUser.LastActive;
                    friend.AppUser2Id = friendId; // TODO: Check it doesn't break any logic
                }
            }
            return friend;
        }
        
                /**
         * Add a new friendship with pending status
         * @param userId is mandatory and represents current user's Id
         */
        public new async Task<BLLAppDTO.FriendshipBLL> Add(BLLAppDTO.FriendshipBLL entity, object? userId = null)
        {
            // UserId is mandatory for adding Friendship
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            // Check if relationship already exists
            var userGuidId = new Guid(userId.ToString());
            var friendId = entity.AppUser2Id == userGuidId ? entity.AppUser1Id : entity.AppUser2Id;
            
            var existingFriendship = await UOW.Friendships.GetForUserAsync(userGuidId, friendId, true);
            var existingPendingRequest = await UOW.Friendships.GetForUserAsync(userGuidId, friendId, false);
            
            if (existingFriendship != null || existingPendingRequest != null)
            {
                // return new BLLAppDTO.FriendshipBLL();
                throw new NotSupportedException($"Could not add friendship - relationship between users {userGuidId.ToString()} {entity.AppUser2Id} already exists");
            }
            
            // Add new relationship with pending status
            entity.IsConfirmed = false;
            entity.AppUser1Id = userGuidId;
            return base.Add(entity, userId);
        }

        /**
         * Change pending friendship to confirmed status
         * @param userId is mandatory and represents current user's Id
         */
        public new async Task<BLLAppDTO.FriendshipBLL> UpdateAsync(BLLAppDTO.FriendshipBLL entity, object? userId = null)
        {
            // UserId is mandatory for updating Friendship
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            // Update relationship to confirmed status
            entity.IsConfirmed = true;
            entity.AppUser1Id = new Guid(userId.ToString());
            return await base.UpdateAsync(entity, userId);
        }
    }
}
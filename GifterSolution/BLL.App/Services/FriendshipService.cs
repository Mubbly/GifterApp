using System;
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

namespace BLL.App.Services
{
    public class FriendshipService : BaseEntityService<IAppUnitOfWork,
            IFriendshipRepository, IFriendshipServiceMapper, DALAppDTO.FriendshipDAL, BLLAppDTO.FriendshipBLL>,
        IFriendshipService
    {
        public FriendshipService(IAppUnitOfWork uow) : base(uow, uow.Friendships, new FriendshipServiceMapper())
        {
        }

        public async Task<IEnumerable<BLLAppDTO.FriendshipBLL>> GetAllConfirmedForUserAsync(Guid userId, bool noTracking = true)
        {
            var userFriendships = await UOW.Friendships.GetAllForUserAsync(userId, true, noTracking);
            return userFriendships.Select(e => Mapper.Map(e));
        }
        
        public async Task<IEnumerable<BLLAppDTO.FriendshipBLL>> GetAllPendingForUserAsync(Guid userId, bool noTracking = true)
        {
            var userFriendships = await UOW.Friendships.GetAllForUserAsync(userId, false, noTracking);
            return userFriendships.Select(e => Mapper.Map(e));
        }

        public async Task<BLLAppDTO.FriendshipBLL> GetConfirmedForUserAsync(Guid userId, Guid friendId, bool noTracking = true)
        {
            var friendship = await UOW.Friendships.GetForUserAsync(userId, friendId, true, noTracking);
            return Mapper.Map(friendship);
        }

        public async Task<BLLAppDTO.FriendshipBLL> GetPendingForUserAsync(Guid userId, Guid friendId, bool noTracking = true)
        {
            var friendship = await UOW.Friendships.GetForUserAsync(userId, friendId, false, noTracking);
            return Mapper.Map(friendship);
        }
        
        public new BLLAppDTO.FriendshipBLL Add(BLLAppDTO.FriendshipBLL entity, object? userId = null)
        {
            // UserId is mandatory for adding Friendship
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var userGuidId = new Guid(userId.ToString());
            var existingFriendship = UOW.Friendships.GetForUserAsync(userGuidId, entity.AppUser2Id, true);
            var existingPendingRequest = UOW.Friendships.GetForUserAsync(userGuidId, entity.AppUser2Id, false);
            if (existingFriendship != null || existingPendingRequest != null)
            {
                // return new BLLAppDTO.FriendshipBLL();
                throw new NotSupportedException($"Could not add friendship - relationship between users {userGuidId.ToString()} {entity.AppUser2Id} already exists");
            }
            // Add relationship with pending status
            entity.IsConfirmed = false;
            return base.Add(entity, userId);
        }

        public new async Task<BLLAppDTO.FriendshipBLL> UpdateAsync(BLLAppDTO.FriendshipBLL entity, object? userId = null)
        {
            // UserId is mandatory for updating Friendship
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            // Update relationship to confirmed status
            entity.IsConfirmed = true;
            return await base.UpdateAsync(entity, userId);
        }
    }
}
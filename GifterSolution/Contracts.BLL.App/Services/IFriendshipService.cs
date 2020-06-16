using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IFriendshipService : IBaseEntityService<BLLAppDTO.FriendshipBLL>,
        IFriendshipRepositoryCustom<BLLAppDTO.FriendshipBLL>
    {
        Task<IEnumerable<BLLAppDTO.FriendshipResponseBLL>> GetAllForUserAsync(Guid userId, bool noTracking = true);
        Task<IEnumerable<BLLAppDTO.FriendshipResponseBLL>> GetAllConfirmedForUserAsync(Guid userId, bool noTracking = true);
        Task<IEnumerable<BLLAppDTO.FriendshipResponseBLL>> GetAllPendingForUserAsync(Guid userId, bool isSent = true, bool noTracking = true);

        Task<BLLAppDTO.FriendshipResponseBLL> GetForUserAsync(Guid userId, Guid friendId, bool noTracking = true);
        Task<BLLAppDTO.FriendshipResponseBLL> GetForUserConfirmedAsync(Guid userId, Guid friendId, bool noTracking = true);
        Task<BLLAppDTO.FriendshipResponseBLL> GetForUserPendingAsync(Guid userId, Guid friendId,
            bool noTracking = true);
        
        new Task<BLLAppDTO.FriendshipBLL> Add(BLLAppDTO.FriendshipBLL entity, object? userId = null);
        new Task<BLLAppDTO.FriendshipBLL> UpdateAsync(BLLAppDTO.FriendshipBLL entity, object? userId = null);
    }
}
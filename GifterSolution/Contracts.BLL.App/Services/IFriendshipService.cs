using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IFriendshipService : IBaseEntityService<BLLAppDTO.FriendshipBLL>,
        IFriendshipRepositoryCustom<BLLAppDTO.FriendshipBLL>
    {
        Task<IEnumerable<BLLAppDTO.FriendshipBLL>> GetAllConfirmedForUserAsync(Guid userId, bool noTracking = true);
        Task<IEnumerable<BLLAppDTO.FriendshipBLL>> GetAllPendingForUserAsync(Guid userId, bool noTracking = true);

        Task<BLLAppDTO.FriendshipBLL> GetConfirmedForUserAsync(Guid userId, Guid friendId, bool noTracking = true);
        Task<BLLAppDTO.FriendshipBLL> GetPendingForUserAsync(Guid userId, Guid friendId, bool noTracking = true);
        new Task<BLLAppDTO.FriendshipBLL> Add(BLLAppDTO.FriendshipBLL entity, object? userId = null);
        new Task<BLLAppDTO.FriendshipBLL> UpdateAsync(BLLAppDTO.FriendshipBLL entity, object? userId = null);
    }
}
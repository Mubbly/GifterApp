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
        Task<IEnumerable<BLLAppDTO.FriendshipBLL>> GetAllPersonalAsync(Guid userId, bool isConfirmed = true, bool noTracking = true);
    }
}
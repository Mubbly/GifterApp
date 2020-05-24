using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IFriendshipRepository : IBaseRepository<DALAppDTO.FriendshipDAL>, IFriendshipRepositoryCustom
    {
        Task<IEnumerable<DALAppDTO.FriendshipDAL>> GetAllPersonalAsync(Guid userId, bool isConfirmed = true, bool noTracking = true);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGiftRepository : IBaseRepository<DALAppDTO.GiftDAL>, IGiftRepositoryCustom
    {
        Task<IEnumerable<DALAppDTO.GiftDAL>> GetAllInWishlistForUserAsync(Guid userId, bool noTracking = true);
        Task<IEnumerable<DALAppDTO.GiftDAL>> GetAllArchivedForUserAsync(Guid userId, bool noTracking = true);
    }
}
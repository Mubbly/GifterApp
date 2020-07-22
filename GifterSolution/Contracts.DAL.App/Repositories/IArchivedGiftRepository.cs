using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IArchivedGiftRepository : IBaseRepository<DALAppDTO.ArchivedGiftDAL>, IArchivedGiftRepositoryCustom
    {
        Task<IEnumerable<DALAppDTO.ArchivedGiftDAL>> GetAllGiftedByUserAsync(Guid userId, bool noTracking = true);
        Task<IEnumerable<DALAppDTO.ArchivedGiftDAL>> GetAllReceivedByUserAsync(Guid userId, bool noTracking = true);
        Task<IEnumerable<DALAppDTO.ArchivedGiftDAL>> GetAllPendingReceivedByUserAsync(Guid userId, bool noTracking = true);
        Task<DALAppDTO.ArchivedGiftDAL> GetGiftedByGiftIdAsync(Guid giftId, Guid? userId = null, bool noTracking = true);
        Task<DALAppDTO.ArchivedGiftDAL> GetReceivedByGiftIdAsync(Guid giftId, Guid? userId = null, bool noTracking = true);
        Task<DALAppDTO.ArchivedGiftDAL> GetPendingReceivedByGiftIdAsync(Guid giftId, Guid? userId = null, bool noTracking = true);
        
    }
}
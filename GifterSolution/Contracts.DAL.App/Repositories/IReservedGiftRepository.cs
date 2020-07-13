using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IReservedGiftRepository : IBaseRepository<DALAppDTO.ReservedGiftDAL>, IReservedGiftRepositoryCustom
    {
        Task<IEnumerable<DALAppDTO.ReservedGiftDAL>> GetAllForUserAsync(Guid userId, bool noTracking = true);

        Task<DALAppDTO.ReservedGiftDAL> GetByGiftId(Guid giftId, Guid? reserverUserId = null,
            bool noTracking = true);

    }
}
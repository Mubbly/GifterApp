using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IReservedGiftService : IBaseEntityService<BLLAppDTO.ReservedGiftFullBLL>,
        IReservedGiftRepositoryCustom<BLLAppDTO.ReservedGiftFullBLL>
    {
        Task<IEnumerable<BLLAppDTO.ReservedGiftResponseBLL>> GetAllForUserAsync(Guid userId, bool noTracking = true);
        Task<BLLAppDTO.ReservedGiftResponseBLL> GetByGiftId(Guid giftId, Guid userId);
        
        // new Task<BLLAppDTO.ReservedGiftBLL> Add(BLLAppDTO.ReservedGiftBLL entity, object? userId = null);
        // Task<BLLAppDTO.ReservedGiftBLL> MarkAsGiftedAsync(BLLAppDTO.ReservedGiftBLL entity, Guid userId);
        // Task<BLLAppDTO.ReservedGiftBLL> CancelReservationAsync(BLLAppDTO.ReservedGiftBLL entity, Guid userId);
    }
}
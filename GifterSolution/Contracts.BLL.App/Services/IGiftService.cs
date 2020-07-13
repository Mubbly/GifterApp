using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IGiftService : IBaseEntityService<BLLAppDTO.GiftBLL>, IGiftRepositoryCustom<BLLAppDTO.GiftBLL>
    {
        Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllForUserAsync(Guid userId, bool noTracking = true);
        Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllPinnedForUserAsync(Guid userId, bool noTracking = true);
        Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllReservedForUserAsync(Guid userId, bool noTracking = true);
        Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllArchivedForUserAsync(Guid userId, bool isGiven = false,
            bool noTracking = true);
        Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllPendingReceivedForUserAsync(Guid userId, bool noTracking = true);

        Task<BLLAppDTO.GiftBLL> GetForUserAsync(Guid giftId, Guid userId, bool noTracking = true);
        Task<BLLAppDTO.GiftBLL> GetReservedForUserAsync(Guid giftId, Guid userId, bool noTracking = true);
        Task<BLLAppDTO.GiftBLL> GetArchivedForUserAsync(Guid giftId, Guid userId, bool isGiven = false,
            bool noTracking = true);
        
        Task<BLLAppDTO.GiftBLL> MarkAsReservedAsync(BLLAppDTO.ReservedGiftBLL entity, Guid userId);
        Task<BLLAppDTO.GiftBLL> MarkAsGiftedAsync(BLLAppDTO.ReservedGiftBLL entity, Guid userId);
        Task<BLLAppDTO.GiftBLL> CancelReservationAsync(BLLAppDTO.ReservedGiftBLL entity, Guid userId);
    }
}
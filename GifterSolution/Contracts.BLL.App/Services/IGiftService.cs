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
        // Get all corresponding to criteria
        Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllInWishlistForUserAsync(Guid userId, bool noTracking = true);
        Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllPinnedForUserAsync(Guid userId, bool noTracking = true);
        Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllReservedForUserAsync(Guid userId, bool noTracking = true);
        Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllArchivedForUserAsync(Guid userId, bool isGiven = false,
            bool noTracking = true);
        Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllPendingArchivedForUserAsync(Guid userId, bool noTracking = true);

        // Get one corresponding to criteria
        Task<BLLAppDTO.GiftBLL> GetForUserAsync(Guid giftId, Guid userId, bool noTracking = true);
        Task<BLLAppDTO.GiftBLL> GetReservedForUserAsync(Guid giftId, Guid userId, bool noTracking = true);
        Task<BLLAppDTO.GiftBLL> GetArchivedForUserAsync(Guid giftId, Guid userId, bool isGiven = false,
            bool noTracking = true);
        Task<BLLAppDTO.GiftBLL> GetPendingArchivedForUserAsync(Guid giftId, Guid userId, bool noTracking = true);
        
        // Actions with Active status Gifts
        Task<BLLAppDTO.GiftBLL> MarkAsReservedAsync(BLLAppDTO.ReservedGiftBLL entity, Guid userId);
        
        // Actions with Reserved status Gifts
        Task<BLLAppDTO.GiftBLL> MarkAsGiftedAsync(BLLAppDTO.ReservedGiftBLL entity, Guid userId);
        Task<BLLAppDTO.GiftBLL> CancelReservationAsync(BLLAppDTO.ReservedGiftBLL entity, Guid userId);
        
        // Actions with Archived status Gifts
        Task<BLLAppDTO.GiftBLL> ConfirmPendingArchivedAsync(BLLAppDTO.ArchivedGiftBLL entity, Guid userId);
        Task<BLLAppDTO.GiftBLL> DenyPendingArchivedAsync(BLLAppDTO.ArchivedGiftBLL entity, Guid userId);
        Task<BLLAppDTO.GiftBLL> DeleteArchivedAsync(BLLAppDTO.ArchivedGiftBLL entity, Guid userId);
        Task<BLLAppDTO.GiftBLL> ReactivateArchivedAsync(BLLAppDTO.ArchivedGiftBLL entity, Guid userId);
    }
}
using System;
using System.Threading.Tasks;
using BLL.App.Helpers;
using BLL.App.Mappers;
using com.mubbly.gifterapp.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Services
{
    public class ArchivedGiftService : BaseEntityService<IAppUnitOfWork,
            IArchivedGiftRepository, IArchivedGiftServiceMapper, DALAppDTO.ArchivedGiftDAL, BLLAppDTO.ArchivedGiftFullBLL>,
        IArchivedGiftService
    {
        // Statuses
        private static string _activeId = "";

        public ArchivedGiftService(IAppUnitOfWork uow) : base(uow, uow.ArchivedGifts, new ArchivedGiftServiceMapper())
        {
            // Get necessary statuses & actionTypes
            var enums = new Enums();
            _activeId = enums.GetStatusId(Enums.Status.Active);
        }
        
        /**
         * Reactivate already received gift
         * Updates corresponding gift to Active status, but keeps the old archivedGift entry.
         * @param userId is mandatory and represents current user's/gift receiver's Id
         */
        public async Task<BLLAppDTO.GiftBLL> ReactivateAsync(BLLAppDTO.ArchivedGiftFullBLL entity, object? userId = null)
        {
            // UserId is mandatory for deleting ArchivedGift
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            
            // Only the gift owner/receiver can reactivate the gift
            if (new Guid(userId.ToString()) != entity.UserReceiverId)
            {
                throw new NotSupportedException(
                    $"Could not reactivate gift {entity.Id} by user {userId}");
            }
            
            // TODO: Call Gift service instead!
            // Update gift to Active status, keep the Archived entry
            var gift = await UOW.Gifts.FirstOrDefaultAsync(entity.GiftId, userId);
            gift.StatusId = new Guid(_activeId);
            var reactivatedGift = await UOW.Gifts.UpdateAsync(gift, userId);
            
            return Mapper.MapGiftToBLL(reactivatedGift);
        }
    }
}
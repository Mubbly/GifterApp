using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Helpers;
using BLL.App.Mappers;
using com.mubbly.gifterapp.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Domain.App;
using PublicApi.DTO.v1;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Services
{
    public class ReservedGiftService : BaseEntityService<IAppUnitOfWork,
            IReservedGiftRepository, IReservedGiftServiceMapper, DALAppDTO.ReservedGiftDAL, BLLAppDTO.ReservedGiftFullBLL>,
        IReservedGiftService
    {
        // Statuses
        private static string _activeId = "";
        private static string _reservedId = "";
        private static string _archivedId = "";
        // Actiontypes
        private static string _archiveId = "";

        public ReservedGiftService(IAppUnitOfWork uow) : base(uow, uow.ReservedGifts, new ReservedGiftServiceMapper())
        {
            // Get necessary statuses & actionTypes
            var enums = new Enums();
            _activeId = enums.GetStatusId(Enums.Status.Active);
            _reservedId = enums.GetStatusId(Enums.Status.Reserved);
            _archivedId = enums.GetStatusId(Enums.Status.Archived);
            _archiveId = enums.GetActionTypeId(Enums.ActionType.Archive);
        }

        public async Task<IEnumerable<BLLAppDTO.ReservedGiftResponseBLL>> GetAllForUserAsync(Guid userId, bool noTracking = true)
        {
            var personalReservedGifts = await UOW.ReservedGifts.GetAllForUserAsync(userId, noTracking);
            return personalReservedGifts.Select(e => Mapper.MapReservedGiftDALToResponseBLL(e));
        }

        public async Task<BLLAppDTO.ReservedGiftResponseBLL> GetByGiftId(Guid giftId, Guid userId)
        {
            // UserId is mandatory for getting ReservedGift. TODO: Same for other ways of getting them?
            if (userId == null)
            { 
                throw new ArgumentNullException(nameof(userId));
            }
            var reservedGift = await UOW.ReservedGifts.GetByGiftId(giftId, userId);
            return Mapper.MapReservedGiftDALToResponseBLL(reservedGift);
        }

        // /**
        //  * Add a new reservedGift. Gift entry will be kept too, but changed to Reserved status.
        //  * 
        //  * @param reserverUserId is mandatory and represents current user's Id
        //  */
        // public new async Task<BLLAppDTO.ReservedGiftBLL> Add(BLLAppDTO.ReservedGiftBLL entity, object? reserverUserId = null)
        // {
        //     // UserId is mandatory for adding ReservedGift
        //     if (reserverUserId == null)
        //     {
        //         throw new ArgumentNullException(nameof(reserverUserId));
        //     }
        //
        //     // Check if this gift exists
        //     var giftReceiverId = entity.UserReceiverId;
        //     if (giftReceiverId == null)
        //     {
        //         throw new NotSupportedException($"Could not reserve gift - UserReceiverId not found");
        //     }
        //     var existingGift = await UOW.Gifts.FirstOrDefaultAsync(entity.GiftId, giftReceiverId);
        //     if (existingGift == null)
        //     {
        //         throw new NotSupportedException($"Could not reserve a gift that does not exist");
        //     }
        //     // Check if this gift is already reserved
        //     if (existingGift.StatusId.ToString() == _reservedId) 
        //     {
        //         throw new NotSupportedException($"Could not reserve gift - this gift {existingGift.Id.ToString()} is already reserved");
        //     }
        //
        //     // Update Gift to reserved status
        //     existingGift.StatusId = new Guid(_reservedId);
        //     var updatedGift = await UOW.Gifts.UpdateAsync(existingGift, giftReceiverId);
        //     // TODO: Check it works
        //     if (updatedGift.StatusId.ToString() != _reservedId)
        //     {
        //         throw new NotSupportedException($"Could not update gift status to Reserved for gift {updatedGift.Id.ToString()}");
        //     }
        //     
        //     // Add a new reservedGift corresponding to updated Gift
        //     entity.ReservedFrom = DateTime.UtcNow;
        //     entity.StatusId = new Guid(_reservedId); // Reserved
        //     entity.ActionTypeId = new Guid(_archiveId); // Archive
        //     entity.UserGiverId = new Guid(reserverUserId.ToString());
        //     return base.Add(entity, reserverUserId); 
        // }
        //
        // /**
        //  * Remove reservedGift - in case it was gifted.
        //  * Inserts new corresponding archivedGift. Gift entry will be kept too, but changed to Archived status.
        //  * ReservedGift will be deleted.
        //  * 
        //  * @param userId is mandatory and represents current user's Id
        //  */
        // public async Task<BLLAppDTO.ReservedGiftBLL> MarkAsGiftedAsync(BLLAppDTO.ReservedGiftBLL entity,
        //     Guid userId)
        // {
        //     // UserId is mandatory for deleting ReservedGift
        //     if (userId == null)
        //     {
        //         throw new ArgumentNullException(nameof(userId));
        //     }
        //
        //     // Only the one who reserved can complete the reservation
        //     if (new Guid(userId.ToString()) != entity.UserGiverId)
        //     {
        //         throw new NotSupportedException(
        //             $"Could not mark reserved gift {entity.Id} as Gifted by this user {userId}");
        //     }
        //
        //     // TODO: Move the code and call ArchivedGiftService.Create or something?
        //     // Check if gift is already archived
        //     var giftAlreadyArchived = UOW.ArchivedGifts.ExistsAsync(entity.Id, entity.UserReceiverId).Result;
        //     if (giftAlreadyArchived)
        //     {
        //         throw new NotSupportedException(
        //             $"Could not mark the reserved gift {entity.Id} as gifted - already archived");
        //     }
        //
        //     // Add new ArchivedGift
        //     var archivedGift = Mapper.MapReservedGiftToArchivedGift(entity);
        //     archivedGift.DateArchived = DateTime.Now;
        //     archivedGift.StatusId = new Guid(_archivedId);
        //     var addedArchivedGift = UOW.ArchivedGifts.Add(archivedGift, userId);
        //     if (addedArchivedGift == null)
        //     {
        //         throw new NotSupportedException($"Could not add ArchivedGift {entity.Id}");
        //     }
        //
        //     // Change corresponding Gift's status to Archived
        //     var gift = await UOW.Gifts.FirstOrDefaultAsync(entity.GiftId, entity.UserReceiverId);
        //     gift.StatusId = new Guid(_archivedId);
        //     var updatedGift = await UOW.Gifts.UpdateAsync(gift, entity.UserReceiverId);
        //     if (updatedGift.StatusId.ToString() != _archivedId)
        //     {
        //         throw new NotSupportedException($"Could not update Gift status to Archived for {entity.GiftId}");
        //     }
        //
        //     // Delete corresponding ReservedGift
        //     return await base.RemoveAsync(entity.Id, userId);
        // }
        //
        // /**
        //  * Remove reservedGift - in case the reservation was cancelled.
        //  * Changes gift status to Active. ReservedGift will be deleted.
        //  * 
        //  * @param userId is mandatory and represents current user's Id
        //  */
        // public async Task<BLLAppDTO.ReservedGiftBLL> CancelReservationAsync(BLLAppDTO.ReservedGiftBLL entity, Guid userId)
        // {
        //     // UserId is mandatory for deleting ReservedGift
        //     if (userId == null)
        //     {
        //         throw new ArgumentNullException(nameof(userId));
        //     }
        //     
        //     // Only the one who reserved can cancel the reservation
        //     if (new Guid(userId.ToString()) != entity.UserGiverId)
        //     {
        //         throw new NotSupportedException(
        //             $"Could not cancel the reserved gift {entity.Id} by this user {userId}");
        //     }
        //     // Change corresponding Gift's status back to Active
        //     var gift = await UOW.Gifts.FirstOrDefaultAsync(entity.GiftId, entity.UserReceiverId);
        //     gift.StatusId = new Guid(_activeId);
        //     var updatedGift = await UOW.Gifts.UpdateAsync(gift, entity.UserReceiverId);
        //     if (updatedGift.StatusId.ToString() != _activeId)
        //     {
        //         throw new NotSupportedException($"Could not update Gift status to Active for {entity.GiftId}");
        //     }
        //     
        //     // Delete corresponding ReservedGift
        //     return await base.RemoveAsync(entity.Id, userId);
        // }
    }
}
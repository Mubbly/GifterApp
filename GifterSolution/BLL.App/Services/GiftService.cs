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
    public class GiftService : BaseEntityService<IAppUnitOfWork,
            IGiftRepository, IGiftServiceMapper, DALAppDTO.GiftDAL, BLLAppDTO.GiftBLL>,
        IGiftService
    {
        // private readonly IReservedGiftService _reservedGiftService;

        // Statuses
        private static string _activeId = "";
        private static string _reservedId = "";
        private static string _archivedId = "";

        public GiftService(IAppUnitOfWork uow) : base(uow, uow.Gifts, new GiftServiceMapper())
        {
            // _reservedGiftService = reservedGiftService;
            // Get necessary statuses & actionTypes
            var enums = new Enums();
            _activeId = enums.GetStatusId(Enums.Status.Active);
            _reservedId = enums.GetStatusId(Enums.Status.Reserved);
            _archivedId = enums.GetStatusId(Enums.Status.Archived);
        }

        public async Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllForUserAsync(Guid userId, bool noTracking = true)
        {
            var userGifts = (await UOW.Gifts.GetAllForUserAsync(userId, noTracking))
                .Select(e => Mapper.Map(e));
            return userGifts;
        }
        
        public async Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllPinnedForUserAsync(Guid userId, bool noTracking = true)
        {
            var pinnedGifts = (await UOW.Gifts.GetAllForUserAsync(userId, noTracking))
                .Where(g => g.IsPinned)
                .Select(g => Mapper.Map(g));
            return pinnedGifts;
        }

        public async Task<BLLAppDTO.GiftBLL> GetForUserAsync(Guid giftId, Guid userId, bool noTracking = true)
        {
            var personalGift = (await GetAllForUserAsync(userId, noTracking))
                .FirstOrDefault(e => e.Id == giftId);
            return personalGift;
        }
        
        
        // -------------------------------------------- RESERVED GIFTS ------------------------------------------
        

        public async Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllReservedForUserAsync(Guid userId, bool noTracking = true)
        {
            var personallyReservedGifts = new List<BLLAppDTO.GiftBLL>();

            // UserId is mandatory for getting ReservedGifts
            if (userId == null)
            { 
                throw new ArgumentNullException(nameof(userId));
            }
            
            // Get ReservedGifts where current user is the reserver
            var personalReservedGifts = await UOW.ReservedGifts.GetAllForUserAsync(userId, noTracking);
            foreach (var personalReservedGift in personalReservedGifts)
            {
                // Only get gifts where reserver is current user
                if (personalReservedGift.UserGiverId != userId)
                {
                    continue;
                }
                // Get gift which is in Reserved status and which id matches the one in ReservedGifts table
                var giftOwner = personalReservedGift.UserReceiverId;
                var personalGift = (await UOW.Gifts.GetAllForUserAsync(giftOwner, noTracking))
                    .Where(g => g.StatusId.ToString().Equals(_reservedId))
                    .Where(g => g.Id == personalReservedGift.GiftId)
                    .Select(g => Mapper.Map(g))
                    .FirstOrDefault();
                if (personalGift == null)
                {
                    continue;
                }
                // Include additional data regarding reservation in the response
                personalGift.UserGiverId = personalReservedGift.UserGiverId;
                personalGift.UserReceiverId = personalReservedGift.UserReceiverId;
                personalGift.UserReceiverName = personalReservedGift.UserReceiver?.FullName;
                personalGift.ReservedFrom = personalReservedGift.ReservedFrom;
                // Add to list
                personallyReservedGifts.Add(personalGift);
            }

            return personallyReservedGifts;
        }
        
        public async Task<BLLAppDTO.GiftBLL> GetReservedForUserAsync(Guid giftId, Guid userId, bool noTracking = true)
        {
            // UserId is mandatory for getting ReservedGift
            if (userId == null)
            { 
                throw new ArgumentNullException(nameof(userId));
            }
            
            // ReservedGift with corresponding GiftId foreign key value has to exist and reserver has to be current user
            var reservedGift = await UOW.ReservedGifts.GetByGiftId(giftId, userId);
            if (giftId != reservedGift.GiftId || reservedGift.UserGiverId != userId)
            {
                throw new NotSupportedException($"Could not find reserved gift with giftId {giftId.ToString()}");
            }
            // Get gift
            var giftOwner = reservedGift.UserReceiverId;
            var gift = Mapper.Map((await UOW.Gifts.GetAllForUserAsync(giftOwner, noTracking))
                .FirstOrDefault(g => g.Id == giftId));
            if (gift == null)
            {
                return null!;
            }
            // Include additional data regarding reservation in the response
            gift.UserGiverId = reservedGift.UserGiverId;
            gift.ReservedFrom = reservedGift.ReservedFrom;
            
            return gift;
        }

        /**
         * Updates Gift status to Reserved and adds a new ReservedGift table entry.
         *
         * @param userId is mandatory and represents current user's Id, will be used as reserverUserId.
         */
        public async Task<BLLAppDTO.GiftBLL> MarkAsReservedAsync(BLLAppDTO.ReservedGiftBLL entity, Guid userId)
        {
            var targetGiftId = entity.GiftId;
            var giftReceiverId = entity.UserReceiverId;
            
            // UserIds are mandatory for adding ReservedGift
            if (userId == null || giftReceiverId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            // Check if this gift even exists
            var existingGift = Mapper.Map(await UOW.Gifts.FirstOrDefaultAsync(targetGiftId, giftReceiverId));
            if (existingGift == null)
            {
                throw new NotSupportedException($"Could not reserve a gift that does not exist");
            }
            // Check existing gift is not already reserved
            if (await IsGiftReservedAsync(existingGift, userId)) 
            {
                throw new NotSupportedException($"Could not reserve gift - {targetGiftId.ToString()} is already reserved");
            }

            // ADD NEW ReservedGift
            var newReservedGift = AddReservedGift(targetGiftId, userId, giftReceiverId);
            if (newReservedGift == null)
            {
                throw new NotSupportedException($"Could not reserve gift {targetGiftId.ToString()} - data insertion fail");
            }
            // UPDATE Gift to reserved status
            var updatedGift = await UpdateGiftStatusToReservedAsync(existingGift, newReservedGift, userId);
            
            return updatedGift;
        }

        /**
         * Remove reservedGift - in case it was gifted.
         * Inserts new corresponding archivedGift. Gift entry will be kept too, but changed to Archived status.
         * ReservedGift will be deleted.
         * 
         * @param userId is mandatory and represents current user's Id
         */
        public async Task<BLLAppDTO.GiftBLL> MarkAsGiftedAsync(BLLAppDTO.ReservedGiftBLL entity, Guid userId)
        {
            var targetGiftId = entity.GiftId;
            var giftReceiverId = entity.UserReceiverId;
            
            // UserIds are mandatory for archiving reserved gift
            if (userId == null || giftReceiverId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            // Check that target gift exists and status is reserved (by current user)
            var gift = Mapper.Map(await UOW.Gifts.FirstOrDefaultAsync(targetGiftId, giftReceiverId));
            var isGiftAlreadyArchived = await IsGiftArchivedAsync(gift, userId, giftReceiverId);
            var isGiftReservedByRequester = await IsGiftReservedAsync(gift, userId);
            
            if (gift == null || isGiftAlreadyArchived || !isGiftReservedByRequester)
            {
                throw new NotSupportedException(
                    $"Could not find reserved Gift {targetGiftId.ToString()} to mark as Gifted by user {userId.ToString()}");
            }

            // ADD NEW ArchivedGift
            var newArchivedGift = AddArchivedGift(targetGiftId, userId, giftReceiverId);
            if (newArchivedGift == null)
            {
                throw new NotSupportedException($"Could not mark gift {targetGiftId.ToString()} as gifted - data insertion fail");
            }
            
            // DELETE corresponding ReservedGift
            var reservedGift = await UOW.ReservedGifts.GetByGiftId(targetGiftId, userId);
            // gift = Mapper.Map(reservedGift.Gift); // Error InvalidOperationException - maybe a workaround? Right now include removed from Repo
            await UOW.ReservedGifts.RemoveAsync(reservedGift, userId);
            
            // UPDATE corresponding Gift's status to Archived
            var updatedGift = await UpdateGiftStatusToArchivedAsync(gift, newArchivedGift, userId);
            
            return updatedGift;
        }
        
        /**
         * Remove reservedGift - in case the reservation was cancelled.
         * Changes gift status to Active. ReservedGift will be deleted.
         * 
         * @param userId is mandatory and represents current user's Id
         */
        public async Task<BLLAppDTO.GiftBLL> CancelReservationAsync(BLLAppDTO.ReservedGiftBLL entity, Guid userId)
        {
            var targetGiftId = entity.GiftId;
            var giftReceiverId = entity.UserReceiverId;
            
            // UserIds are mandatory for deleting ReservedGift
            if (userId == null || giftReceiverId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            // Check gift is Reserved and by current user
            var gift = await UOW.Gifts.FirstOrDefaultAsync(targetGiftId, giftReceiverId);
            if (!(await IsGiftReservedAsync(Mapper.Map(gift), userId)))
            {
                throw new NotSupportedException(
                    $"Could not find a reserved gift {targetGiftId.ToString()} to cancel reservation by user {userId.ToString()}");
            }

            // DELETE corresponding ReservedGift
            var reservedGift = await UOW.ReservedGifts.GetByGiftId(targetGiftId, userId);
            // gift = Mapper.Map(reservedGift.Gift); // Error InvalidOperationException - maybe a workaround? Right now include removed from Repo
            await UOW.ReservedGifts.RemoveAsync(reservedGift, userId);

            // UPDATE corresponding Gift's status back to Active
            var reactivatedGift = await UpdateGiftStatusToActiveAsync(Mapper.Map(gift), userId);
            
            return reactivatedGift;
        }

        /**
         * Gift has to be in Reserved status and corresponding entry in ReservedGifts table
         * Note: True only if requester/current user is the one who reserved the gift - ensured in DAL.
         */
        private async Task<bool> IsGiftReservedAsync(BLLAppDTO.GiftBLL gift, Guid userId)
        {
            var targetGiftId = gift.Id;
            var reservedGift = await UOW.ReservedGifts.GetByGiftId(targetGiftId, userId);
            
            var isGiftStatusReserved = gift != null && gift.StatusId.ToString().Equals(_reservedId);
            var doesReservedGiftsHaveAnEntry = isGiftStatusReserved && reservedGift != null;

            return isGiftStatusReserved && doesReservedGiftsHaveAnEntry;
        }

        /**
         * Gift has to be in Archived status and corresponding entry in ArchivedGifts table
         * Note: Counting pending Archived gifts in too.
         */
        private async Task<bool> IsGiftArchivedAsync(BLLAppDTO.GiftBLL gift, Guid userId, Guid receiverId)
        {
            var targetGiftId = gift.Id;
            var archivedGiftByReserver = await UOW.ArchivedGifts.GetGiftedByGiftIdAsync(targetGiftId, userId);
            var archivedGiftByOwner = await UOW.ArchivedGifts.GetReceivedByGiftIdAsync(targetGiftId, receiverId);
            var archivedPendingGift = await UOW.ArchivedGifts.GetPendingReceivedByGiftIdAsync(targetGiftId, userId);

            var isGiftStatusArchived = (gift != null && gift.StatusId.ToString().Equals(_archivedId));
            var doesArchivedGiftsHaveEntry = archivedPendingGift != null || archivedGiftByOwner != null ||
                                             archivedGiftByReserver != null;
            
            return isGiftStatusArchived && doesArchivedGiftsHaveEntry;
        }

        /** Add new entry to ReservedGift's table */
        private BLLAppDTO.ReservedGiftResponseBLL? AddReservedGift(Guid giftId, Guid userId, Guid receiverId)
        {
            // Create
            var reservedGiftBLL = new BLLAppDTO.ReservedGiftFullBLL
            {
                StatusId = new Guid(_reservedId),
                ActionTypeId = new Guid(_archivedId),
                GiftId = giftId,
                UserGiverId = userId,
                UserReceiverId = receiverId,
                ReservedFrom = DateTime.UtcNow
            };
            // Add
            var newReservedGift = UOW.ReservedGifts.Add(Mapper.MapReservedGiftFullToDAL(reservedGiftBLL), userId); 
            // Track
            UOW.AddToEntityTracker(newReservedGift, reservedGiftBLL);
            // Check added data
            var isReservationSuccessful = reservedGiftBLL.GiftId == giftId;
            
            return newReservedGift != null && isReservationSuccessful ? Mapper.MapReservedGiftFullToResponse(reservedGiftBLL) : null;
        }
                
        /** Add new entry to ArchivedGifts table */
        private BLLAppDTO.ArchivedGiftResponseBLL? AddArchivedGift(Guid giftId, Guid userId, Guid receiverId)
        {
            // Create
            var archivedGiftBLL = new BLLAppDTO.ArchivedGiftFullBLL
            {
                StatusId = new Guid(_archivedId),
                ActionTypeId = new Guid(_activeId),
                GiftId = giftId,
                UserGiverId = userId,
                UserReceiverId = receiverId,
                DateArchived = DateTime.UtcNow,
                IsConfirmed = false
            };
            // Add
            var newArchivedGift = UOW.ArchivedGifts.Add(Mapper.MapArchivedGiftFullToDAL(archivedGiftBLL), userId); 
            // Track
            UOW.AddToEntityTracker(newArchivedGift, archivedGiftBLL);
            // Check added data
            var isArchivalSuccessful = archivedGiftBLL.GiftId == giftId;
    
            return newArchivedGift != null && isArchivalSuccessful ? Mapper.MapArchivedGiftFullToResponse(archivedGiftBLL) : null; 
        }

        /** Update gift to Reserved status if corresponding ReservedGift exists */
        private async Task<BLLAppDTO.GiftBLL> UpdateGiftStatusToReservedAsync(BLLAppDTO.GiftBLL gift, BLLAppDTO.ReservedGiftResponseBLL reservedGift, Guid userId)
        {
            var targetGiftId = gift.Id;

            if (targetGiftId != reservedGift.GiftId)
            {
                throw new NotSupportedException($"Could not reserve gift {targetGiftId.ToString()}  - data insertion fail");
            }
            // Update gift's status
            gift.StatusId = new Guid(_reservedId);
            var updatedGift = Mapper.Map(await UOW.Gifts.UpdateAsync(Mapper.Map(gift), reservedGift.UserReceiverId));
            
            if (updatedGift == null)
            {
                return null!;
            }
            // Include new data regarding reservation in response
            updatedGift.ReservedFrom = reservedGift.ReservedFrom;
            updatedGift.UserGiverId = userId;
            return updatedGift;
        }

        /** Update gift to Archived status if corresponding ArchivedGift exists */
        private async Task<BLLAppDTO.GiftBLL> UpdateGiftStatusToArchivedAsync(BLLAppDTO.GiftBLL gift, BLLAppDTO.ArchivedGiftResponseBLL archivedGift, Guid userId)
        {
            var targetGiftId = gift.Id;
            
            if (targetGiftId != archivedGift.GiftId)
            {
                throw new NotSupportedException($"Could not reserve gift {targetGiftId.ToString()}  - data insertion fail");
            }
            // Update gift's status
            gift.StatusId = new Guid(_archivedId);
            var updatedGift = Mapper.Map(await UOW.Gifts.UpdateAsync(Mapper.Map(gift), archivedGift.UserReceiverId));

            if (updatedGift == null)
            {
                return null!;
            }
            // Include new data regarding reservation in response
            updatedGift.ReservedFrom = archivedGift.DateArchived;
            updatedGift.UserGiverId = userId;
            return updatedGift;
        }
        
        /** Update gift to Active status if corresponding ReservedGift exists */
        private async Task<BLLAppDTO.GiftBLL> UpdateGiftStatusToActiveAsync(BLLAppDTO.GiftBLL gift, Guid userId)
        {
            var targetGiftId = gift.Id;
            
            // Check if target gift is reserved by current user
            var reservedGift = await UOW.ReservedGifts.GetByGiftId(targetGiftId, userId);
            if (reservedGift == null)
            {
                throw new NotSupportedException($"Could not reactivate gift {gift.Id.ToString()} - no reservation found");
            }
            // Update gift's status to Active
            gift.StatusId = new Guid(_activeId);
            var updatedGift = Mapper.Map(await UOW.Gifts.UpdateAsync(Mapper.Map(gift), reservedGift.UserReceiverId));

            if (updatedGift == null)
            {
                return null!;
            }
            // Include new data regarding activation in response
            updatedGift.ReservedFrom = null;
            updatedGift.UserGiverId = null;
            return updatedGift;
        }
        
        
        // -------------------------------------------- ARCHIVED GIFTS ------------------------------------------

        
        public async Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllArchivedForUserAsync(Guid userId, bool isGiven = false, bool noTracking = true)
        {
            // UserId is mandatory for getting ArchivedGifts
            if (userId == null)
            { 
                throw new ArgumentNullException(nameof(userId));
            }
            // Gifted by current user to other people
            if (isGiven)
            {
                return await GetAllGivenArchivedAsync(userId, noTracking);
            }
            // Gifted by other people to current user
            return await GetAllReceivedArchivedAsync(userId, noTracking);
        }
        
        public async Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllPendingReceivedForUserAsync(Guid userId, bool noTracking = true)
        {
            // UserId is mandatory for getting ArchivedGifts
            if (userId == null)
            { 
                throw new ArgumentNullException(nameof(userId));
            }
            // Gifted by other people to current user, but not yet confirmed by current user
            return await GetAllPendingReceivedAsync(userId, noTracking);
        }
        
        public async Task<BLLAppDTO.GiftBLL> GetArchivedForUserAsync(Guid giftId, Guid userId, bool isGiven = false,
            bool noTracking = true)
        {
            // UserId is mandatory for getting ArchivedGift
            if (userId == null)
            { 
                throw new ArgumentNullException(nameof(userId));
            }
            // Gifted by current user to another person
            if (isGiven)
            {
                return await GetGivenArchivedAsync(giftId, userId, noTracking);
            }
            // Gifted by another person to current user
            return await GetReceivedArchivedAsync(giftId, userId, noTracking);
        }

        /** Other people's Gifts - the ones current user has already gifted */
        private async Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllGivenArchivedAsync(Guid userId, bool noTracking = false)
        {
            var givenGifts = new List<BLLAppDTO.GiftBLL>();
            var givenArchivedGifts = await UOW.ArchivedGifts.GetAllGiftedByUserAsync(userId, noTracking);
            foreach (var givenArchivedGift in givenArchivedGifts)
            {
                // Only get gifts where giver is current user
                if (givenArchivedGift.UserGiverId != userId)
                {
                    continue;
                }
                var giftOwner = givenArchivedGift.UserReceiverId;
                var archivedGift = (await UOW.Gifts.GetAllForUserAsync(giftOwner, noTracking))
                    .Where(g => g.StatusId.ToString().Equals(_archivedId))
                    .Where(g => g.Id == givenArchivedGift.GiftId)
                    .Select(g => Mapper.Map(g))
                    .FirstOrDefault();
                
                if (archivedGift == null)
                {
                    continue;
                }
                // Include additional data regarding archival in the response
                archivedGift.ArchivedFrom = givenArchivedGift.DateArchived;
                archivedGift.UserReceiverId = givenArchivedGift.UserReceiverId;
                archivedGift.UserReceiverName = givenArchivedGift.UserReceiver?.FullName;
                // Add to list
                givenGifts.Add(archivedGift);
            }
            return givenGifts;
        }

        /** Current user's personal Gifts that others have already gifted to them and they have confirmed it */
        private async Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllReceivedArchivedAsync(Guid userId,
            bool noTracking = false)
        {
            var receivedGifts = new List<BLLAppDTO.GiftBLL>();
            var receivedArchivedGifts = await UOW.ArchivedGifts.GetAllReceivedByUserAsync(userId, noTracking);
            foreach (var receivedArchivedGift in receivedArchivedGifts)
            {
                // Only get gifts where receiver is current user
                if (receivedArchivedGift.UserReceiverId != userId)
                {
                    continue;
                }
                var giftOwner = receivedArchivedGift.UserReceiverId;
                var archivedGift = (await UOW.Gifts.GetAllForUserAsync(giftOwner, noTracking))
                    .Where(g => g.StatusId.ToString().Equals(_archivedId))
                    .Where(g => g.Id == receivedArchivedGift.GiftId)
                    .Select(g => Mapper.Map(g))
                    .FirstOrDefault();
                
                if (archivedGift == null)
                {
                    continue;
                }
                // Include additional data regarding archival in the response
                archivedGift.ArchivedFrom = receivedArchivedGift.DateArchived;
                archivedGift.UserGiverId = receivedArchivedGift.UserGiverId;
                archivedGift.UserGiverName = receivedArchivedGift.UserGiver?.FullName;
                // Add to list
                receivedGifts.Add(archivedGift);
            }
            return receivedGifts;
        }
        
        /** Current user's personal Gifts that others have already gifted to them but they haven't confirmed yet */
        private async Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllPendingReceivedAsync(Guid userId,
            bool noTracking = false)
        {
            var pendingReceivedGifts = new List<BLLAppDTO.GiftBLL>();
            var unconfirmedReceivedGifts = await UOW.ArchivedGifts.GetAllPendingReceivedByUserAsync(userId, noTracking);
            foreach (var unconfirmedGift in unconfirmedReceivedGifts)
            {
                // Only get gifts where receiver is current user
                if (unconfirmedGift.UserReceiverId != userId)
                {
                    continue;
                }
                var giftOwnerId = unconfirmedGift.UserReceiverId;
                var pendingGift = (await UOW.Gifts.GetAllForUserAsync(giftOwnerId, noTracking))
                    .Where(g => g.StatusId.ToString().Equals(_archivedId))
                    .Where(g => g.Id == unconfirmedGift.GiftId)
                    .Select(g => Mapper.Map(g))
                    .FirstOrDefault();
                
                var pendingGift2 = (await UOW.Gifts.GetAllForUserAsync(giftOwnerId, noTracking))
                    .Where(g => g.StatusId.ToString().Equals(_archivedId))
                    .Select(g => Mapper.Map(g))
                    .FirstOrDefault();
                
                var pendingGift3 = (await UOW.Gifts.GetAllForUserAsync(giftOwnerId, noTracking))
                    .Where(g => g.Id == unconfirmedGift.GiftId)
                    .Select(g => Mapper.Map(g))
                    .FirstOrDefault();
                
                var pendingGift4 = (await UOW.Gifts.GetAllForUserAsync(giftOwnerId, noTracking)).FirstOrDefault();
                
                if (pendingGift == null)
                {
                    continue;
                }
                // Include additional data regarding archival in the response
                pendingGift.ArchivedFrom = unconfirmedGift.DateArchived;
                pendingGift.UserGiverId = unconfirmedGift.UserGiverId;
                pendingGift.UserGiverName = unconfirmedGift.UserGiver?.FullName;
                // Add to list
                pendingReceivedGifts.Add(pendingGift);
            }
            return pendingReceivedGifts;
        }
        
        private async Task<BLLAppDTO.GiftBLL> GetGivenArchivedAsync(Guid giftId, Guid userId, bool noTracking = true)
        {
            // ArchivedGift with corresponding GiftId foreign key value has to exist and giver has to be current user
            var givenArchivedGift = await UOW.ArchivedGifts.GetGiftedByGiftIdAsync(giftId, userId, noTracking);
            if (giftId != givenArchivedGift.GiftId || givenArchivedGift.UserGiverId != userId)
            {
                throw new NotSupportedException($"Could not find archived gift with giftId {giftId.ToString()}");
            }
            // Get gift
            var giftOwner = givenArchivedGift.UserReceiverId;
            var givenGift = Mapper.Map((await UOW.Gifts.GetAllForUserAsync(giftOwner, noTracking))
                .FirstOrDefault(g => g.Id == giftId));

            if (givenGift == null)
            {
                return null!;
            }
            // Include additional data regarding archival in the response
            givenGift.ArchivedFrom = givenArchivedGift.DateArchived;
            givenGift.UserReceiverId = givenArchivedGift.UserReceiverId;
            givenGift.UserReceiverName = givenArchivedGift.UserReceiver?.FullName;

            return givenGift;
        }

        private async Task<BLLAppDTO.GiftBLL> GetReceivedArchivedAsync(Guid giftId, Guid userId, bool noTracking = true)
        {
            // ArchivedGift with corresponding GiftId foreign key value has to exist and receiver has to be current user
            var receivedArchivedGift = await UOW.ArchivedGifts.GetReceivedByGiftIdAsync(giftId, userId);
            if (giftId != receivedArchivedGift.GiftId || receivedArchivedGift.UserReceiverId != userId)
            {
                throw new NotSupportedException($"Could not find archived gift with giftId {giftId.ToString()}");
            }
            // Get gift
            var giftOwner = receivedArchivedGift.UserReceiverId;
            var receivedGift = Mapper.Map((await UOW.Gifts.GetAllForUserAsync(giftOwner, noTracking))
                .FirstOrDefault(g => g.Id == giftId));

            if (receivedGift == null)
            {
                return null!;
            }
            // Include additional data regarding archival in the response
            receivedGift.ArchivedFrom = receivedArchivedGift.DateArchived;
            receivedGift.UserGiverId = receivedArchivedGift.UserGiverId;
            receivedGift.UserGiverName = receivedArchivedGift.UserGiver?.FullName;

            return receivedGift;
        }
    }
}
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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Identity;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Services
{
    public class ProfileService : BaseEntityService<IAppUnitOfWork,
            IProfileRepository, IProfileServiceMapper, DALAppDTO.ProfileDAL, BLLAppDTO.ProfileBLL>,
        IProfileService
    {
        // Statuses
        private static string _activeId = "";
        private static string _reservedId = "";
        private static string _archivedId = "";

        public ProfileService(IAppUnitOfWork uow) : base(uow, uow.Profiles, new ProfileServiceMapper())
        {
            // Get necessary statuses & actionTypes
            var enums = new Enums();
            _activeId = enums.GetStatusId(Enums.Status.Active);
            _reservedId = enums.GetStatusId(Enums.Status.Reserved);
            _archivedId = enums.GetStatusId(Enums.Status.Archived);
        }
        
        /* Gets just the profile data */
        public async Task<BLLAppDTO.ProfileBLL> GetByUserAsync(Guid userId, Guid accessingUserId, Guid? profileId = null, bool noTracking = true)
        {
            var userProfile = await UOW.Profiles.GetByUserAsync(userId, profileId, noTracking);
            return Mapper.Map(userProfile);
        }

        /* Gets profile with user data, wishlist and gifts */
        public async Task<BLLAppDTO.ProfileBLL> GetFullByUserAsync(Guid userId, Guid accessingUserId, Guid? profileId = null, bool noTracking = true)
        {
            var fullProfile = Mapper.Map(await UOW.Profiles.GetFullByUserAsync(userId, profileId, noTracking));

            // Return profile as usual when there are no gifts or only Active ones
            var hasOnlyActiveGifts = fullProfile?.Wishlist?.Gifts != null && fullProfile!.Wishlist!.Gifts.All(g => g.StatusId.ToString().Equals(_activeId));
            if (fullProfile?.Wishlist?.Gifts == null || hasOnlyActiveGifts)
            {
                return fullProfile!; // ignore null error
            }
            
            // If there are any Archived gifts, remove them before returning profile
            var hasAnyArchivedGifts = fullProfile.Wishlist.Gifts.Any(g => g.StatusId.ToString().Equals(_archivedId));
            if (hasAnyArchivedGifts)
            {
                var archivedGifts = fullProfile.Wishlist.Gifts
                    .Where(g => g.StatusId.ToString().Equals(_archivedId))
                    .ToList();
                foreach (var gift in archivedGifts)
                {
                    fullProfile.Wishlist.Gifts.Remove(gift);
                }
            }
            
            // If there are any Reserved gifts, include additional info before returning profile
            var hasAnyReservedGifts = fullProfile.Wishlist.Gifts.Any(g => g.StatusId.ToString().Equals(_reservedId));
            if (hasAnyReservedGifts)
            {
                // Get ReservedGifts data
                var reservedGifts = (await UOW.ReservedGifts.GetAllAsync(userId))
                    .Where(rg => 
                        fullProfile.Wishlist.Gifts
                            .Select(g => g.Id)
                            .Contains(rg.GiftId))
                    .ToList();
                
                // Get gifts in Reserved status
                var giftsInReservedStatus = fullProfile.Wishlist.Gifts
                    .Where(g => g.StatusId.ToString().Equals(_reservedId))
                    .ToList();
                
                // For each Gift in Reserved status, include some data from corresponding ReservedGift
                foreach (var gift in giftsInReservedStatus)
                {
                    var reservedGift = reservedGifts
                        .Where(rg => rg.GiftId == gift.Id)
                        .Select(rg => Mapper.MapReservedGiftToBLL(rg))
                        .First();
                
                    // Include reserving date - everyone can see when the gift was reserved
                    gift.ReservedFrom = reservedGift.ReservedFrom;
                    
                    // Include reserver user's id for logged in user - reserver can see which gifts are theirs, but not who reserved other gifts
                    if (reservedGift.UserGiverId == accessingUserId)
                    {
                        gift.UserGiverId = reservedGift.UserGiverId;
                    }
                }
            }
            
            return fullProfile!; // ignore null error
        }

        /**
         *  Create a new basic profile with an empty wishlist
         *  NB: DOES NOT CHECK WHETHER A PROFILE ALREADY EXISTS! Do not call if not sure!
         */
        public BLLAppDTO.ProfileBLL CreateDefaultProfile(Guid userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            // Initialize default profile with default empty wishlist (foreign key dependency)
            var newDefaultWishlist = AddDefaultWishlist(userId);
            var newDefaultProfile = AddDefaultProfile(userId, newDefaultWishlist);
            return newDefaultProfile;
        }

        /** Initial wishlist for a new user with no existing profiles */
        private BLLAppDTO.WishlistBLL AddDefaultWishlist(Guid userId)
        {
            var defaultWishlistBLL = new BLLAppDTO.WishlistBLL
            {
                AppUserId = userId
            };
            // Add
            var defaultWishlistDAL = Mapper.MapWishlistToDAL(defaultWishlistBLL);
            var defaultWishlistDALTracked = UOW.Wishlists.Add(defaultWishlistDAL);
            // Track
            UOW.AddToEntityTracker(defaultWishlistDALTracked, defaultWishlistBLL);
            return defaultWishlistBLL;
        }
        
        /** Initial profile for a new user with no existing profiles */
        private BLLAppDTO.ProfileBLL AddDefaultProfile(Guid userId, BLLAppDTO.WishlistBLL wishlist)
        {
            var defaultProfileBLL = new BLLAppDTO.ProfileBLL
            {
                AppUserId = userId,
                Wishlist = wishlist
            };
            // Add
            var defaultProfileDAL = Mapper.Map(defaultProfileBLL);
            var defaultProfileDALTracked = UOW.Profiles.Add(defaultProfileDAL);
            // Track
            UOW.AddToEntityTracker(defaultProfileDALTracked, defaultProfileBLL);
            return defaultProfileBLL;
        }
    }
}
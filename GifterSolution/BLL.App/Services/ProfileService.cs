﻿using System;
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

            // Return profile as usual when there are no gifts
            if (fullProfile?.Wishlist?.Gifts == null)
            {
                return fullProfile!; // ignore null error
            }
            
            // Get reserved gifts data
            var reservedGifts = (await UOW.ReservedGifts.GetAllAsync(userId))
                .Where(rg => 
                    fullProfile.Wishlist.Gifts
                        .Select(g => g.Id)
                        .Contains(rg.GiftId))
                .ToList();

            // Return profile as usual when there are no reserved gifts and no archived gifts
            if (!reservedGifts.Any() && !fullProfile.Wishlist.Gifts.Any(g => g.StatusId.ToString().Equals(_archivedId)))
            {
                return fullProfile!; // ignore null error
            }
            if (!reservedGifts.Any())
            {
                return fullProfile!; // ignore null error
            }

            // Additional logic for Reserved and Archived Gifts
            foreach (var gift in fullProfile.Wishlist.Gifts.ToList())
            {
                // Exclude archived Gifts from Profile
                var isGiftArchived = gift.StatusId.ToString().Equals(_archivedId);
                if (isGiftArchived)
                {
                    fullProfile.Wishlist.Gifts.Remove(gift);
                }
                // Add more info for Reserved Gifts
                var isGiftReserved = gift.StatusId.ToString().Equals(_reservedId);
                if (isGiftReserved)
                {
                    var reservedGift = reservedGifts
                        .Where(rg => rg.GiftId == gift.Id)
                        .Select(rg => Mapper.MapReservedGiftToBLL(rg))
                        .First();
                    
                    // Everyone can see when the gift was reserved
                    gift.ReservedFrom = reservedGift.ReservedFrom;
                    
                    // Reserver can see which gifts are theirs, but not who reserved other gifts
                    if (reservedGift.UserGiverId == accessingUserId)
                    {
                        gift.UserGiverId = reservedGift.UserGiverId;
                    }
                }
            }
            // Return profile with new data for reserved gifts
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
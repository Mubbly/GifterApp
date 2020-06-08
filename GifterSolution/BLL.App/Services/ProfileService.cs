using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using com.mubbly.gifterapp.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
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
        public ProfileService(IAppUnitOfWork uow) : base(uow, uow.Profiles, new ProfileServiceMapper())
        {
            
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
            var userProfile = await UOW.Profiles.GetFullByUserAsync(userId, profileId, noTracking);
            
            // if (userProfile.IsPrivate)
            // {
            //     // Only friends can see profile
            //     var isRequestingUserFriend = UOW.Friendships.GetConfirmedForUserAsync(userId, accessingUserId) != null;
            //     if (!isRequestingUserFriend)
            //     {
            //         userProfile = null;
            //     }
            // }

            return Mapper.Map(userProfile);
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
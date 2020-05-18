using System;
using System.Threading.Tasks;
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
    public class ProfileService : BaseEntityService<IAppUnitOfWork,
            IProfileRepository, IProfileServiceMapper, DALAppDTO.ProfileDAL, BLLAppDTO.ProfileBLL>,
        IProfileService
    {
        public ProfileService(IAppUnitOfWork uow) : base(uow, uow.Profiles, new ProfileServiceMapper())
        {
            
        }

        public async Task<BLLAppDTO.ProfileBLL> GetPersonalAsync(Guid userId, Guid? profileId, bool noTracking = true)
        {
            var existingProfile = await UOW.Profiles.GetPersonalAsync(userId, profileId, noTracking);
            
            // Trying to get default profile but receiving null
            if (profileId == null && existingProfile == null)
            {
                // Initialize default profile with default wishlist
                var newDefaultWishlist = AddDefaultWishlist(userId);
                var newDefaultProfile = AddDefaultProfile(userId, newDefaultWishlist.Id);
                await UOW.SaveChangesAsync();
                var newDefaultProfileWithUserData = await UOW.Profiles.GetPersonalAsync(userId, newDefaultProfile.Id);
                return Mapper.Map(newDefaultProfileWithUserData);
                // await UOW.SaveChangesAsync();
                // return newDefaultProfile;
            }
            // Existing or with some wrong profileId provided
            return Mapper.Map(existingProfile);
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
            return Mapper.MapWishlistToBLL(defaultWishlistDALTracked);
        }
        
        /** Initial profile for a new user with no existing profiles */
        private BLLAppDTO.ProfileBLL AddDefaultProfile(Guid userId, Guid wishlistId)
        {
            var defaultProfileBLL = new BLLAppDTO.ProfileBLL
            {
                AppUserId = userId,
                WishlistId = wishlistId
            };
            // Add
            var defaultProfileDAL = Mapper.Map(defaultProfileBLL);
            var defaultProfileDALTracked = UOW.Profiles.Add(defaultProfileDAL);
            // Track
            UOW.AddToEntityTracker(defaultProfileDALTracked, defaultProfileBLL);
            var newDefaultProfileBLL = Mapper.Map(defaultProfileDALTracked);
            
            //var newDefaultProfileWithUserData = await UOW.Profiles.GetPersonalAsync(userId, newDefaultProfileBLL.Id);
            return newDefaultProfileBLL;
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using com.mubbly.gifterapp.DAL.Base.EF.Repositories;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using Microsoft.EntityFrameworkCore;
using DomainApp = Domain.App;
using DALAppDTO = DAL.App.DTO;
using DomainAppIdentity = Domain.App.Identity;

namespace DAL.App.EF.Repositories
{
    public class ProfileRepository :
        EFBaseRepository<AppDbContext, DomainAppIdentity.AppUser, DomainApp.Profile, DALAppDTO.ProfileDAL>,
        IProfileRepository
    {
        public ProfileRepository(AppDbContext dbContext) :
            base(dbContext, new DALMapper<DomainApp.Profile, DALAppDTO.ProfileDAL>())
        {
        }

        public async Task<DALAppDTO.ProfileDAL> GetFullByUserAsync(Guid userId, Guid? profileId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var profiles = 
                query
                .Where(a => a.AppUserId == userId)
                .OrderBy(e => e.CreatedAt)
                .Include(a => a.AppUser)
                .Include(a => a.Wishlist)
                    .ThenInclude(w => w!.Gifts)
                .Select(e => Mapper.Map(e));
            var profilesList = await profiles.ToListAsync();
            var debug = profilesList.FirstOrDefault().Wishlist.Gifts;
            // If no specific profile ID provided, just get the first one
            return profileId == null
                ? profilesList.FirstOrDefault()
                : profilesList.FirstOrDefault(p => p.Id == profileId);
        }
        
        public async Task<DALAppDTO.ProfileDAL> GetByUserAsync(Guid userId, Guid? profileId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var profiles =
                await query
                    .Where(a => a.AppUserId == userId)
                    .OrderBy(e => e.CreatedAt)
                    .Select(e => Mapper.Map(e))
                    .ToListAsync();
            // If no specific profile ID provided, just get the first one
            return profileId == null
                ? profiles.FirstOrDefault()
                : profiles.FirstOrDefault(p => p.Id == profileId);
        }

        // public async Task<IEnumerable<Profile>> AllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(p => p.Wishlist)
        //         .Include(p => p.AppUser)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // See your own profile
        //         query = query.Where(p => p.AppUserId == userId);
        //     }
        //
        //     return await query.ToListAsync();
        // }
        //
        // public async Task<Profile> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(p => p.Wishlist)
        //         .Include(p => p.AppUser)
        //         .Where(p => p.Id == id)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // See your own profile
        //         query = query.Where(p => p.AppUserId == userId);
        //     }
        //
        //     return await query.FirstOrDefaultAsync();
        // }
        //
        // public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        // {
        //     if (userId != null)
        //     {
        //         return await RepoDbSet.AnyAsync(p => p.Id == id && p.AppUserId == userId);
        //     }
        //     return await RepoDbSet.AnyAsync(p => p.Id == id);
        // }
        //
        // public async Task DeleteAsync(Guid id, Guid? userId = null)
        // {
        //     var profile = await FirstOrDefaultAsync(id, userId);
        //     base.Remove(profile);
        // }
        //
        // public async Task<IEnumerable<ProfileDTO>> DTOAllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(p => p.Wishlist)
        //         .Include(p => p.AppUser)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // See your own profile
        //         query = query.Where(p => p.AppUserId == userId);
        //     }
        //     
        //     return await query
        //         .Select(p => new ProfileDTO() 
        //         {
        //             Id = p.Id,
        //             Bio = p.Bio,
        //             Age = p.Age,
        //             Gender = p.Gender,
        //             ProfilePicture = p.ProfilePicture,
        //             IsPrivate = p.IsPrivate,
        //             WishlistId = p.WishlistId,
        //             AppUserId = p.AppUserId,
        //             UserProfilesCount = p.UserProfiles.Count
        //         }).ToListAsync();
        // }
        //
        // public async Task<ProfileDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(p => p.Wishlist)
        //         .Include(p => p.AppUser)
        //         .Where(p => p.Id == id)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // See your own profile
        //         query = query.Where(p => p.AppUserId == userId);
        //     }
        //     
        //     return await query
        //         .Select(p => new ProfileDTO() 
        //         {
        //             Id = p.Id,
        //             Bio = p.Bio,
        //             Age = p.Age,
        //             Gender = p.Gender,
        //             ProfilePicture = p.ProfilePicture,
        //             IsPrivate = p.IsPrivate,
        //             WishlistId = p.WishlistId,
        //             AppUserId = p.AppUserId,
        //             UserProfilesCount = p.UserProfiles.Count
        //         }).FirstOrDefaultAsync();
        // }
    }
}
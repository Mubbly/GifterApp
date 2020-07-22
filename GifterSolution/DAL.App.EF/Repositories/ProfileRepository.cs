using System;
using System.Linq;
using System.Threading.Tasks;
using com.mubbly.gifterapp.DAL.Base.EF.Repositories;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Helpers;
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
        // private static string _archivedId = "";

        public ProfileRepository(AppDbContext dbContext) :
            base(dbContext, new DALMapper<DomainApp.Profile, DALAppDTO.ProfileDAL>())
        {
            // Get necessary statuses & actionTypes
            // var enums = new Enums();
            // _archivedId = enums.GetStatusId(Enums.Status.Archived);
        }

        // TODO: Wth is up with page loading time (~3sec) using includes? https://github.com/dotnet/efcore/issues/18022 ?
        public async Task<DALAppDTO.ProfileDAL> GetFullByUserAsync(Guid userId, Guid? profileId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            var profiles = await query
                .Where(p => p.AppUserId == userId)
                .OrderBy(p => p.CreatedAt)
                .Include(p => p.AppUser)
                .Include(p => p.Wishlist)
                    .ThenInclude(w => w!.Gifts)
                // .Where(p=> p.Wishlist!.Gifts.Any(g => g.StatusId.ToString().Equals(_archivedId)))
                .ToListAsync();
            
            // If no specific profile ID provided, just get the first one
            return profileId == null 
                ? Mapper.Map(profiles.FirstOrDefault()) 
                : Mapper.Map(profiles.FirstOrDefault(p => p.Id == profileId));
        }
        
        public async Task<DALAppDTO.ProfileDAL> GetByUserAsync(Guid userId, Guid? profileId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            
            var profiles = await query
                    .Where(p => p.AppUserId == userId)
                    .OrderBy(p => p.CreatedAt)
                    .Select(p => Mapper.Map(p))
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
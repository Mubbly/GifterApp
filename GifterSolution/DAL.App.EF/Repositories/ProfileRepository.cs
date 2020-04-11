using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class ProfileRepository : EFBaseRepository<Profile, AppDbContext>, IProfileRepository
    {
        public ProfileRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Profile>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(p => p.Wishlist)
                .Include(p => p.AppUser)
                .AsQueryable();

            if (userId != null)
            {
                // See your own profile
                query = query.Where(p => p.AppUserId == userId);
            }

            return await query.ToListAsync();
        }

        public async Task<Profile> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(p => p.Wishlist)
                .Include(p => p.AppUser)
                .Where(p => p.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                // See your own profile
                query = query.Where(p => p.AppUserId == userId);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId != null)
            {
                return await RepoDbSet.AnyAsync(p => p.Id == id && p.AppUserId == userId);
            }
            return await RepoDbSet.AnyAsync(p => p.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var profile = await FirstOrDefaultAsync(id, userId);
            base.Remove(profile);
        }

        public async Task<IEnumerable<ProfileDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(p => p.Wishlist)
                .Include(p => p.AppUser)
                .AsQueryable();

            if (userId != null)
            {
                // See your own profile
                query = query.Where(p => p.AppUserId == userId);
            }
            
            return await query
                .Select(p => new ProfileDTO() 
                {
                    Id = p.Id,
                    Bio = p.Bio,
                    Age = p.Age,
                    Gender = p.Gender,
                    ProfilePicture = p.ProfilePicture,
                    IsPrivate = p.IsPrivate,
                    WishlistId = p.WishlistId,
                    AppUserId = p.AppUserId,
                    UserProfilesCount = p.UserProfiles.Count
                }).ToListAsync();
        }

        public async Task<ProfileDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(p => p.Wishlist)
                .Include(p => p.AppUser)
                .Where(p => p.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                // See your own profile
                query = query.Where(p => p.AppUserId == userId);
            }
            
            return await query
                .Select(p => new ProfileDTO() 
                {
                    Id = p.Id,
                    Bio = p.Bio,
                    Age = p.Age,
                    Gender = p.Gender,
                    ProfilePicture = p.ProfilePicture,
                    IsPrivate = p.IsPrivate,
                    WishlistId = p.WishlistId,
                    AppUserId = p.AppUserId,
                    UserProfilesCount = p.UserProfiles.Count
                }).FirstOrDefaultAsync();
        }
    }
}
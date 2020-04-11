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
                .Include(g => g.Wishlist)
                .Include(g => g.AppUser)
                .AsQueryable();

            if (userId != null)
            {
                query = query.Where(g => g.AppUserId == userId);
            }

            return await query.ToListAsync();
        }

        public Task<Profile> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id, Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProfileDTO>> DTOAllAsync(Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<ProfileDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            throw new NotImplementedException();
        }
    }
}
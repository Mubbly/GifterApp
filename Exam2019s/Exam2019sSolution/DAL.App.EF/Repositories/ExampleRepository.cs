using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.mubbly.gifterapp.DAL.Base.EF.Repositories;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ExampleRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Example, ExampleDAL>,
        IExampleRepository
    {
        public ExampleRepository(AppDbContext dbContext) :
            base(dbContext, new DALMapper<Domain.App.Example, ExampleDAL>())
        {
        }
        
        /** Include user data */
        public async Task<IEnumerable<ExampleDAL>> GetAllWithUserDataAsync(object? userId = null, bool noTracking = true)
        {
            var examples = PrepareQuery(userId, noTracking);
            var examplesWithUserInfo =
                await examples
                    .Include(e => e.AppUser)
                    .OrderBy(e => e.CreatedAt)
                    .Select(e => Mapper.Map(e))
                    .ToListAsync();
            return examplesWithUserInfo;
        }
        
        /** Include user data */
        public async Task<ExampleDAL> GetWithUserDataAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            var example = await query
                .Where(e => e.Id == id)
                .Include(e => e.AppUser)
                .OrderBy(e => e.CreatedAt)
                .FirstOrDefaultAsync();
            
            return Mapper.Map(example);
        }
        
        /** Filter per user, include user data */
        public async Task<IEnumerable<ExampleDAL>> GetAllForUserAsync(Guid userId, bool noTracking = true)
        {
            // User's examples
            var examples = PrepareQuery(userId, noTracking);
            var personalExamples =
                await examples
                    .Where(e => e.AppUserId == userId)
                    .Include(e => e.AppUser)
                    .OrderBy(e => e.CreatedAt)
                    .Select(e => Mapper.Map(e))
                    .ToListAsync();
            return personalExamples;
        }

        /** Filter per user, include user data */
        public async Task<ExampleDAL> GetForUserAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            var example = await query
                .Where(e => e.Id == id && e.AppUserId == userId)
                .Include(e => e.AppUser)
                .OrderBy(e => e.CreatedAt)
                .FirstOrDefaultAsync();
            
            return Mapper.Map(example);
        }
    }
}
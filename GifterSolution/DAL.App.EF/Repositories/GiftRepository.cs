using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class GiftRepository : EFBaseRepository<Gift, AppDbContext>, IGiftRepository
    {
        public GiftRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        /*
        // Return only gifts that start with the letter "a" - random override example
        public override async Task<IEnumerable<Gift>> AllAsync()
        {
            return await RepoDbSet.Where(g => g.Name.StartsWith("a")).ToListAsync();
        }
        */
        
    }
}
using com.mubbly.gifterapp.DAL.Base.EF.Repositories;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;

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
        
        // Example:
        // public async Task<IEnumerable<DALAppDTO.FriendshipDAL>> GetAllForUserAsync(Guid userId, bool isConfirmed = true,
        //     bool noTracking = true)
        // {
        //     // User's friendships
        //     var friendships = PrepareQuery(userId, noTracking);
        //     var personalFriendships =
        //         await friendships
        //             .Where(e => e.IsConfirmed == isConfirmed && (e.AppUser1Id == userId || e.AppUser2Id == userId))
        //             .OrderBy(e => e.CreatedAt)
        //             .Select(e => Mapper.Map(e))
        //             .ToListAsync();
        //     return personalFriendships;
        // }
    }
}
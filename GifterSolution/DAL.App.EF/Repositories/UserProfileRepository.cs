using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories
{
    public class UserProfileRepository : EFBaseRepository<UserProfile, AppDbContext>, IUserProfileRepository
    {
        public UserProfileRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
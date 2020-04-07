using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories
{
    public class UserCampaignRepository : EFBaseRepository<UserCampaign, AppDbContext>, IUserCampaignRepository
    {
        public UserCampaignRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
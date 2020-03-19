using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class UserCampaignRepository : BaseRepository<UserCampaign>, IUserCampaignRepository
    {
        public UserCampaignRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
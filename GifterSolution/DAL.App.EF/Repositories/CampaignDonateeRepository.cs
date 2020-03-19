using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CampaignDonateeRepository : BaseRepository<CampaignDonatee>, ICampaignDonateeRepository
    {
        public CampaignDonateeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
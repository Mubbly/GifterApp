using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CampaignDonateeRepository : EFBaseRepository<CampaignDonatee, AppDbContext>, ICampaignDonateeRepository
    {
        public CampaignDonateeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories
{
    public class ActionTypeRepository : EFBaseRepository<ActionType, AppDbContext>, IActionTypeRepository
    {
        public ActionTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
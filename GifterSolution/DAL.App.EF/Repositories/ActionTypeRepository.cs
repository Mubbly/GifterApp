using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ActionTypeRepository : BaseRepository<ActionType>, IActionTypeRepository
    {
        public ActionTypeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
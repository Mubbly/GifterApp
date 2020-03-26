using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class StatusRepository : EFBaseRepository<Status, AppDbContext>, IStatusRepository
    {
        public StatusRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
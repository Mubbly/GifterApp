using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class DonateeRepository : EFBaseRepository<Donatee, AppDbContext>, IDonateeRepository
    {
        public DonateeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories
{
    public class PermissionRepository : EFBaseRepository<Permission, AppDbContext>, IPermissionRepository
    {
        public PermissionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
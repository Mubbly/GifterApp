using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class UserPermissionRepository : EFBaseRepository<UserPermission, AppDbContext>, IUserPermissionRepository
    {
        public UserPermissionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
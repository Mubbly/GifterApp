using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.App.Repositories.Identity;
using DAL.Base.EF.Repositories;
using Domain;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories.Identity
{
    public class AppUserRepository : EFBaseRepository<AppUser, AppDbContext>, IAppUserRepository
    {
        public AppUserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
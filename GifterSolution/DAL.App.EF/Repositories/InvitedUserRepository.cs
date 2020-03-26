using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class InvitedUserRepository : EFBaseRepository<InvitedUser, AppDbContext>, IInvitedUserRepository
    {
        public InvitedUserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
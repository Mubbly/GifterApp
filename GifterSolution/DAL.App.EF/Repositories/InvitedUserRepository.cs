using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class InvitedUserRepository : BaseRepository<InvitedUser>, IInvitedUserRepository
    {
        public InvitedUserRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
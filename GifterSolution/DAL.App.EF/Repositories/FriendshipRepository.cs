using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class FriendshipRepository : EFBaseRepository<Friendship, AppDbContext>, IFriendshipRepository
    {
        public FriendshipRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories
{
    public class PrivateMessageRepository : EFBaseRepository<PrivateMessage, AppDbContext>, IPrivateMessageRepository
    {
        public PrivateMessageRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
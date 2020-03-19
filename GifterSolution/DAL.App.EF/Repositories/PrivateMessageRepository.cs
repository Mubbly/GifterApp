using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PrivateMessageRepository : BaseRepository<PrivateMessage>, IPrivateMessageRepository
    {
        public PrivateMessageRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
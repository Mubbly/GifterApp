using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class GiftRepository : BaseRepository<Gift>, IGiftRepository
    {
        public GiftRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
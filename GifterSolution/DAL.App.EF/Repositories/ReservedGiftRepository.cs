using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF.Repositories
{
    public class ReservedGiftRepository : EFBaseRepository<ReservedGift, AppDbContext>, IReservedGiftRepository
    {
        public ReservedGiftRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
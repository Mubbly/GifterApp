using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class WishlistRepository : EFBaseRepository<Wishlist, AppDbContext>, IWishlistRepository
    {
        public WishlistRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
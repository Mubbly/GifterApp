using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IWishlistRepository : IBaseRepository<DALAppDTO.Wishlist>, IWishlistRepositoryCustom
    {
    }
}
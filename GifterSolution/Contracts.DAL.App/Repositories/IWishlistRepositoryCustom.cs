using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IWishlistRepositoryCustom : IWishlistRepositoryCustom<DALAppDTO.Wishlist>
    {
    }

    public interface IWishlistRepositoryCustom<TWishlist>
    {
    }
}
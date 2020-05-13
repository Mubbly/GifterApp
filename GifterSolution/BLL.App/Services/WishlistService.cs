using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Services
{
    public class WishlistService : BaseEntityService<IAppUnitOfWork,
            IWishlistRepository, IWishlistServiceMapper, DALAppDTO.Wishlist, BLLAppDTO.Wishlist>,
        IWishlistService
    {
        public WishlistService(IAppUnitOfWork uow) : base(uow, uow.Wishlists, new WishlistServiceMapper())
        {
        }
    }
}
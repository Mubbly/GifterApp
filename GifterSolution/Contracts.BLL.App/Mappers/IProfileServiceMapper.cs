using com.mubbly.gifterapp.Contracts.BLL.Base.Mappers;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IProfileServiceMapper : IBaseMapper<DALAppDTO.ProfileDAL, BLLAppDTO.ProfileBLL>
    {
        BLLAppDTO.ReservedGiftFullBLL MapReservedGiftToBLL(DALAppDTO.ReservedGiftDAL inObject);
        BLLAppDTO.GiftBLL MapGiftToBLL(DALAppDTO.GiftDAL inObject);
        DALAppDTO.GiftDAL MapGiftToDAL(BLLAppDTO.GiftBLL inObject);
        BLLAppDTO.WishlistBLL MapWishlistToBLL(DALAppDTO.WishlistDAL inObject);
        DALAppDTO.WishlistDAL MapWishlistToDAL(BLLAppDTO.WishlistBLL inObject);
    }
}
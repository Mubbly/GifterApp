using Contracts.BLL.App.Mappers;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class ProfileServiceMapper : BLLMapper<DALAppDTO.ProfileDAL, BLLAppDTO.ProfileBLL>, IProfileServiceMapper
    {
        public BLLAppDTO.ReservedGiftFullBLL MapReservedGiftToBLL(DALAppDTO.ReservedGiftDAL inObject)
        {
            return Mapper.Map<BLLAppDTO.ReservedGiftFullBLL>(inObject);
        }
        
        public BLLAppDTO.GiftBLL MapGiftToBLL(DALAppDTO.GiftDAL inObject)
        {
            return Mapper.Map<BLLAppDTO.GiftBLL>(inObject);
        }
        
        public DALAppDTO.GiftDAL MapGiftToDAL(BLLAppDTO.GiftBLL inObject)
        {
            return Mapper.Map<DALAppDTO.GiftDAL>(inObject);
        }
        
        public BLLAppDTO.WishlistBLL MapWishlistToBLL(DALAppDTO.WishlistDAL inObject)
        {
            return Mapper.Map<BLLAppDTO.WishlistBLL>(inObject);
        }

        public DALAppDTO.WishlistDAL MapWishlistToDAL(BLLAppDTO.WishlistBLL inObject)
        {
            return Mapper.Map<DALAppDTO.WishlistDAL>(inObject);
        }
    }
}
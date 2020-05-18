using com.mubbly.gifterapp.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class ProfileServiceMapper : BLLMapper<DALAppDTO.ProfileDAL, BLLAppDTO.ProfileBLL>, IProfileServiceMapper
    {
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
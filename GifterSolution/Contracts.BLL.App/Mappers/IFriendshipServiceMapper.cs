using BLL.App.DTO.Identity;
using com.mubbly.gifterapp.Contracts.BLL.Base.Mappers;
using DAL.App.DTO.Identity;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IFriendshipServiceMapper : IBaseMapper<DALAppDTO.FriendshipDAL, BLLAppDTO.FriendshipBLL>
    {
        BLLAppDTO.FriendshipResponseBLL MapFriendshipToResponseBLL(DALAppDTO.FriendshipDAL inObject);

        // AppUserBLL Map(AppUserDAL friendshipAppUser1);
    }
}
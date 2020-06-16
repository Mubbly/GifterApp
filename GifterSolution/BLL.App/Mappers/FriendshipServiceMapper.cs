using Contracts.BLL.App.Mappers;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class FriendshipServiceMapper : BLLMapper<DALAppDTO.FriendshipDAL, BLLAppDTO.FriendshipBLL>,
        IFriendshipServiceMapper
    {
        public BLLAppDTO.FriendshipResponseBLL MapFriendshipToResponseBLL(DALAppDTO.FriendshipDAL inObject)
        {
            return Mapper.Map<BLLAppDTO.FriendshipResponseBLL>(inObject);
        }
    }
}
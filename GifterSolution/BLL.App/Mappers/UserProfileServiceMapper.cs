using Contracts.BLL.App.Mappers;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class UserProfileServiceMapper : BLLMapper<DALAppDTO.UserProfileDAL, BLLAppDTO.UserProfileBLL>,
        IUserProfileServiceMapper
    {
    }
}
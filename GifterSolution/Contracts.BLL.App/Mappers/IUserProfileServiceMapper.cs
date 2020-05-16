using Contracts.BLL.Base.Mappers;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IUserProfileServiceMapper : IBaseMapper<DALAppDTO.UserProfileDAL, BLLAppDTO.UserProfileBLL>
    {
    }
}
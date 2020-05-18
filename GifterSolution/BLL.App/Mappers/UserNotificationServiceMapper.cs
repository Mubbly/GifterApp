using Contracts.BLL.App.Mappers;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class UserNotificationServiceMapper : BLLMapper<DALAppDTO.UserNotificationDAL, BLLAppDTO.UserNotificationBLL>,
        IUserNotificationServiceMapper
    {
    }
}
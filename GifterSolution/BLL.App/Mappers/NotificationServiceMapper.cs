using Contracts.BLL.App.Mappers;
using PublicApi.DTO.v1;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class NotificationServiceMapper : BLLMapper<DALAppDTO.NotificationDAL, BLLAppDTO.NotificationBLL>,
        INotificationServiceMapper
    {
        public BLLAppDTO.UserNotificationBLL MapUserNotificationEditToBLL(BLLAppDTO.UserNotificationEditBLL inObject)
        {
            return Mapper.Map<BLLAppDTO.UserNotificationBLL>(inObject);
        }
        
        public BLLAppDTO.UserNotificationBLL MapUserNotificationDALToBLL(DALAppDTO.UserNotificationDAL inObject)
        {
            return Mapper.Map<BLLAppDTO.UserNotificationBLL>(inObject);
        }
    }
}
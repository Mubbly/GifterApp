using BLLAppDTO = BLL.App.DTO;

namespace PublicApi.DTO.v1.Mappers
{
    public class NotificationMapper : BaseMapper<BLLAppDTO.NotificationBLL, NotificationDTO>
    {
        public UserNotificationDTO MapUserNotificationBLLToDTO(BLLAppDTO.UserNotificationBLL inObject)
        {
            return Mapper.Map<UserNotificationDTO>(inObject);
        }
        
        public BLLAppDTO.UserNotificationBLL MapUserNotificationEditToBLL(UserNotificationEditDTO inObject)
        {
            return Mapper.Map<BLLAppDTO.UserNotificationBLL>(inObject);
        }
    }
}
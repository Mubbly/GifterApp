using Contracts.BLL.App.Mappers;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Mappers
{
    public class NotificationTypeServiceMapper : BLLMapper<DALAppDTO.NotificationTypeDAL, BLLAppDTO.NotificationTypeBLL>,
        INotificationTypeServiceMapper
    {
    }
}
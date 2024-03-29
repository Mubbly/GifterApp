﻿using com.mubbly.gifterapp.Contracts.BLL.Base.Mappers;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface INotificationServiceMapper : IBaseMapper<DALAppDTO.NotificationDAL, BLLAppDTO.NotificationBLL>
    {
        BLLAppDTO.UserNotificationBLL MapUserNotificationEditToBLL(BLLAppDTO.UserNotificationEditBLL inObject);
        BLLAppDTO.UserNotificationBLL MapUserNotificationDALToBLL(DALAppDTO.UserNotificationDAL inObject);
    }
}
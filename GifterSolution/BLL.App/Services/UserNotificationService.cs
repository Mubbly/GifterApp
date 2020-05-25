﻿using BLL.App.Mappers;
using com.mubbly.gifterapp.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Services
{
    public class UserNotificationService : BaseEntityService<IAppUnitOfWork,
            IUserNotificationRepository, IUserNotificationServiceMapper, DALAppDTO.UserNotificationDAL,
            BLLAppDTO.UserNotificationBLL>,
        IUserNotificationService
    {
        public UserNotificationService(IAppUnitOfWork uow) : base(uow, uow.UserNotifications,
            new UserNotificationServiceMapper())
        {
        }
    }
}
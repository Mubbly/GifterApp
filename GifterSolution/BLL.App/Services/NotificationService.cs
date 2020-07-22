using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using com.mubbly.gifterapp.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Services
{
    public class NotificationService : BaseEntityService<IAppUnitOfWork,
            INotificationRepository, INotificationServiceMapper, DALAppDTO.NotificationDAL, BLLAppDTO.NotificationBLL>,
        INotificationService
    {
        public NotificationService(IAppUnitOfWork uow) : base(uow, uow.Notifications, new NotificationServiceMapper())
        {
        }

        public async Task<IEnumerable<BLLAppDTO.UserNotificationBLL>> GetAllPersonalNew(Guid userId, bool noTracking = true)
        {
            var newPersonalNotifications = (await UOW.UserNotifications.GetAllActiveForUser(userId, noTracking))
                .Select(e => Mapper.MapUserNotificationDALToBLL(e));
            return newPersonalNotifications;
        }
    }
}
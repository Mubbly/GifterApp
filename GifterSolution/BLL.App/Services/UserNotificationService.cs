using System;
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
    public class UserNotificationService : BaseEntityService<IAppUnitOfWork,
            IUserNotificationRepository, IUserNotificationServiceMapper, DALAppDTO.UserNotificationDAL,
            BLLAppDTO.UserNotificationBLL>,
        IUserNotificationService
    {
        public UserNotificationService(IAppUnitOfWork uow) : base(uow, uow.UserNotifications,
            new UserNotificationServiceMapper())
        {
        }
        
        /**
         * Change active notification to inactive status
         * @param userId is mandatory and represents current user's Id
         */
        public new async Task<BLLAppDTO.UserNotificationBLL> UpdateAsync(BLLAppDTO.UserNotificationBLL entity, object? userId = null)
        {
            // UserId is mandatory for updating Notification
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            // Update notification to inactive status
            entity.IsActive = false;
            entity.AppUserId = new Guid(userId.ToString());
            entity.NotificationId = entity.NotificationId;
            return await base.UpdateAsync(entity, userId);
        }
    }
}
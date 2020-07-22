using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface INotificationService : IBaseEntityService<BLLAppDTO.NotificationBLL>,
        INotificationRepositoryCustom<BLLAppDTO.NotificationBLL>
    {
        Task<IEnumerable<BLLAppDTO.UserNotificationBLL>> GetAllPersonalNew(Guid userId, bool noTracking = true);
    }
}
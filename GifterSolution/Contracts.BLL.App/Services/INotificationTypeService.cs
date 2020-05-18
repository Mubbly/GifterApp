using com.mubbly.gifterapp.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface INotificationTypeService : IBaseEntityService<BLLAppDTO.NotificationTypeBLL>,
        INotificationTypeRepositoryCustom<BLLAppDTO.NotificationTypeBLL>
    {
    }
}
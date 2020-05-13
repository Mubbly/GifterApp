using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface INotificationRepositoryCustom : INotificationRepositoryCustom<DALAppDTO.Notification>
    {
    }

    public interface INotificationRepositoryCustom<TNotification>
    {
    }
}
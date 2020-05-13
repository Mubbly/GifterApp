using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface INotificationTypeRepositoryCustom : INotificationTypeRepositoryCustom<DALAppDTO.NotificationType>
    {
    }

    public interface INotificationTypeRepositoryCustom<TNotificationType>
    {
    }
}
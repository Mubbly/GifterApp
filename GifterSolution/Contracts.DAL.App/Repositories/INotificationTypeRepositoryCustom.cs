using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface INotificationTypeRepositoryCustom : INotificationTypeRepositoryCustom<DALAppDTO.NotificationTypeDAL>
    {
    }

    public interface INotificationTypeRepositoryCustom<TNotificationType>
    {
    }
}
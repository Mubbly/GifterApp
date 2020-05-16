using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserNotificationRepositoryCustom : IUserNotificationRepositoryCustom<DALAppDTO.UserNotificationDAL>
    {
    }

    public interface IUserNotificationRepositoryCustom<TUserNotification>
    {
    }
}
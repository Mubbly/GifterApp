using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserPermissionRepositoryCustom : IUserPermissionRepositoryCustom<DALAppDTO.UserPermission>
    {
    }

    public interface IUserPermissionRepositoryCustom<TUserPermission>
    {
    }
}
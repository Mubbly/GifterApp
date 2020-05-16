using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserPermissionRepositoryCustom : IUserPermissionRepositoryCustom<DALAppDTO.UserPermissionDAL>
    {
    }

    public interface IUserPermissionRepositoryCustom<TUserPermission>
    {
    }
}
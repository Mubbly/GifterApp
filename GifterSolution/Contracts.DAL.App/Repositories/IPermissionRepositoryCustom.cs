using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPermissionRepositoryCustom : IPermissionRepositoryCustom<DALAppDTO.PermissionDAL>
    {
    }

    public interface IPermissionRepositoryCustom<TPermission>
    {
    }
}
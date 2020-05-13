using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPermissionRepositoryCustom : IPermissionRepositoryCustom<DALAppDTO.Permission>
    {
    }

    public interface IPermissionRepositoryCustom<TPermission>
    {
    }
}
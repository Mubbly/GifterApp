using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Services
{
    public class UserPermissionService : BaseEntityService<IAppUnitOfWork,
            IUserPermissionRepository, IUserPermissionServiceMapper, DALAppDTO.UserPermissionDAL, BLLAppDTO.UserPermissionBLL
        >,
        IUserPermissionService
    {
        public UserPermissionService(IAppUnitOfWork uow) : base(uow, uow.UserPermissions,
            new UserPermissionServiceMapper())
        {
        }
    }
}
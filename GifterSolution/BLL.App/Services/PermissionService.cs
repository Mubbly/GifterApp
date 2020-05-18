using BLL.App.Mappers;
using com.mubbly.gifterapp.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Services
{
    public class PermissionService : BaseEntityService<IAppUnitOfWork,
            IPermissionRepository, IPermissionServiceMapper, DALAppDTO.PermissionDAL, BLLAppDTO.PermissionBLL>,
        IPermissionService
    {
        public PermissionService(IAppUnitOfWork uow) : base(uow, uow.Permissions, new PermissionServiceMapper())
        {
        }
    }
}
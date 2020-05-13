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
    public class ActionTypeService : BaseEntityService<IAppUnitOfWork,
            IActionTypeRepository, IActionTypeServiceMapper, DALAppDTO.ActionType, BLLAppDTO.ActionType>,
        IActionTypeService
    {
        public ActionTypeService(IAppUnitOfWork uow) : base(uow, uow.ActionTypes, new ActionTypeServiceMapper())
        {
        }
    }
}
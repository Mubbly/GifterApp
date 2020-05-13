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
    public class DonateeService : BaseEntityService<IAppUnitOfWork,
            IDonateeRepository, IDonateeServiceMapper, DALAppDTO.Donatee, BLLAppDTO.Donatee>,
        IDonateeService
    {
        public DonateeService(IAppUnitOfWork uow) : base(uow, uow.Donatees, new DonateeServiceMapper())
        {
        }
    }
}
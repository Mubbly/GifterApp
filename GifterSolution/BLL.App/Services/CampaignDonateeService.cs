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
    public class CampaignDonateeService : BaseEntityService<IAppUnitOfWork,
            ICampaignDonateeRepository, ICampaignDonateeServiceMapper, DALAppDTO.CampaignDonateeDAL,
            BLLAppDTO.CampaignDonateeBLL>,
        ICampaignDonateeService
    {
        public CampaignDonateeService(IAppUnitOfWork uow) : base(uow, uow.CampaignDonatees,
            new CampaignDonateeServiceMapper())
        {
        }
    }
}
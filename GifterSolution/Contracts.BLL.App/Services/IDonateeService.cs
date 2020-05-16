using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IDonateeService : IBaseEntityService<BLLAppDTO.DonateeBLL>,
        IDonateeRepositoryCustom<BLLAppDTO.DonateeBLL>
    {
        Task<IEnumerable<BLLAppDTO.DonateeBLL>> GetAllForCampaignAsync(Guid campaignId, Guid? userId, bool noTracking = true);

        BLLAppDTO.DonateeBLL Add(BLLAppDTO.DonateeBLL bllDonatee, Guid campaignId, Guid userId);
    }
}
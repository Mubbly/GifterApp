using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface ICampaignService : IBaseEntityService<BLLAppDTO.CampaignBLL>,
        ICampaignRepositoryCustom<BLLAppDTO.CampaignBLL>
    { 
        Task<IEnumerable<BLLAppDTO.CampaignBLL>> GetAllPersonalAsync(Guid userId,
            bool noTracking = true);

        new BLLAppDTO.CampaignBLL Add(BLLAppDTO.CampaignBLL bllCampaign, object? userId = null);
    }
}
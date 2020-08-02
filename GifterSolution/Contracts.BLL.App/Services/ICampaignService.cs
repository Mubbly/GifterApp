using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface ICampaignService : IBaseEntityService<BLLAppDTO.CampaignBLL>,
        ICampaignRepositoryCustom<BLLAppDTO.CampaignBLL>
    {
        Task<IEnumerable<BLLAppDTO.CampaignBLL>> GetAllPersonalAsync(Guid userId,
            bool noTracking = true);
        
        Task<BLLAppDTO.CampaignBLL> GetPersonalAsync(Guid campaignId,
            Guid userId, bool noTracking = true);

        new Task<IEnumerable<BLLAppDTO.CampaignBLL>> GetAllAsync(object? userId, bool noTracking = true);

        new BLLAppDTO.CampaignBLL Add(BLLAppDTO.CampaignBLL bllCampaign, object? userId = null);
    }
}
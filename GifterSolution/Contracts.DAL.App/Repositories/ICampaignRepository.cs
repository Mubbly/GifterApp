using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICampaignRepository : IBaseRepository<DALAppDTO.CampaignDAL>, ICampaignRepositoryCustom
    {
        new Task<IEnumerable<DALAppDTO.CampaignDAL>> GetAllAsync(object? userId = null,
            bool noTracking = true);
        Task<IEnumerable<DALAppDTO.CampaignDAL>> GetAllPersonalAsync(Guid userId, bool noTracking = true);
    }
}
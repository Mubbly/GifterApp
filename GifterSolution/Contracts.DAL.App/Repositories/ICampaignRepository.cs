using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICampaignRepository : IBaseRepository<DALAppDTO.CampaignDAL>, ICampaignRepositoryCustom
    {
        Task<IEnumerable<DALAppDTO.CampaignDAL>> GetAllPersonalAsync(Guid userId, bool noTracking = true);
    }
}
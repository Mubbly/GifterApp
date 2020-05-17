using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IDonateeRepository : IBaseRepository<DALAppDTO.DonateeDAL>, IDonateeRepositoryCustom
    {
        Task<IEnumerable<DALAppDTO.DonateeDAL>> GetAllForCampaignAsync(Guid campaignId, Guid? userId,
            bool noTracking = true);
    }
}
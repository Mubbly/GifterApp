using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface ICampaignDonateeRepository : IBaseRepository<CampaignDonatee>
    {
        Task<IEnumerable<CampaignDonatee>> AllAsync(Guid? userId = null);
        Task<CampaignDonatee> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        Task<IEnumerable<CampaignDonateeDTO>> DTOAllAsync(Guid? userId = null);
        Task<CampaignDonateeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
    }
}
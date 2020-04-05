using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface ICampaignRepository : IBaseRepository<Campaign>
    {
        Task<IEnumerable<Campaign>> AllAsync(Guid? userId = null);
        Task<Campaign> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);

        // DTO methods
        Task<IEnumerable<CampaignDTO>> DTOAllAsync(Guid? userId = null);
        Task<CampaignDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);    
    }
}
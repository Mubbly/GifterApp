using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IStatusRepository : IBaseRepository<Status>
    {
        Task<IEnumerable<Status>> AllAsync(Guid? userId = null);
        Task<Status> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);

        // DTO methods
        Task<IEnumerable<StatusDTO>> DTOAllAsync(Guid? userId = null);
        Task<StatusDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);     
    }
}
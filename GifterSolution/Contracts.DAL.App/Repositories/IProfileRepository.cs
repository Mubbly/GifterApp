using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IProfileRepository : IBaseRepository<Profile>
    {
        Task<IEnumerable<Profile>> AllAsync(Guid? userId = null);
        Task<Profile> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);

        // DTO methods
        Task<IEnumerable<ProfileDTO>> DTOAllAsync(Guid? userId = null);
        Task<ProfileDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);     
    }
}
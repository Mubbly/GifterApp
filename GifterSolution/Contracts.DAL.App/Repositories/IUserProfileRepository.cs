using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserProfileRepository : IBaseRepository<UserProfile>
    {
        Task<IEnumerable<UserProfile>> AllAsync(Guid? userId = null);
        Task<UserProfile> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);

        // DTO methods
        Task<IEnumerable<UserProfileDTO>> DTOAllAsync(Guid? userId = null);
        Task<UserProfileDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);     
    }
}
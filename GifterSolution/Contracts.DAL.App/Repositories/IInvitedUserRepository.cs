using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IInvitedUserRepository : IBaseRepository<InvitedUser>
    {
        Task<IEnumerable<InvitedUser>> AllAsync(Guid? userId = null);
        Task<InvitedUser> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);

        // DTO methods
        Task<IEnumerable<InvitedUserDTO>> DTOAllAsync(Guid? userId = null);
        Task<InvitedUserDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);     
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IFriendshipRepository : IBaseRepository<Friendship>
    {
        Task<IEnumerable<Friendship>> AllAsync(Guid? userId = null);
        Task<Friendship> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);

        // DTO methods
        Task<IEnumerable<FriendshipDTO>> DTOAllAsync(Guid? userId = null);
        Task<FriendshipDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);     
    }
}
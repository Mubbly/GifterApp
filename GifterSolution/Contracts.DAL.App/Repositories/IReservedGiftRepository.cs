using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IReservedGiftRepository : IBaseRepository<ReservedGift>
    {
        Task<IEnumerable<ReservedGift>> AllAsync(Guid? userId = null);
        Task<ReservedGift> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);

        // DTO methods
        Task<IEnumerable<ReservedGiftDTO>> DTOAllAsync(Guid? userId = null);
        Task<ReservedGiftDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);     
    }
}
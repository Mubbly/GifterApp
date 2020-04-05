using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace Contracts.DAL.App.Repositories
{
    public interface IArchivedGiftRepository : IBaseRepository<ArchivedGift>
    {
        Task<IEnumerable<ArchivedGift>> AllAsync(Guid? userId = null);
        Task<ArchivedGift> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
        // DTO methods
        Task<IEnumerable<ArchivedGiftDTO>> DTOAllAsync(Guid? userId = null);
        Task<ArchivedGiftDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null);
        
        // // TODO: Should these be just overrides from AllAsync and FindAsync?
        // public Task<IEnumerable<ArchivedGift>> AllAsyncWithConnectedData();
        // public Task<ArchivedGift> FindAsyncWithConnectedData(params object[] id);
        //
        // public Task<IEnumerable<ActionType>> GetArchivedGiftActionType();
        // public Task<IEnumerable<Gift>> GetArchivedGiftGift();
        // public Task<IEnumerable<Status>> GetArchivedGiftStatus();
        // public Task<IEnumerable<AppUser>> GetArchivedGiftGiverUser();
        // public Task<IEnumerable<AppUser>> GetArchivedGiftReceiverUser();
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using Domain.Identity;

namespace Contracts.DAL.App.Repositories
{
    public interface IArchivedGiftRepository : IBaseRepository<ArchivedGift>
    {
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
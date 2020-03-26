using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ArchivedGiftRepository : EFBaseRepository<ArchivedGift, AppDbContext>, IArchivedGiftRepository
    {
        public ArchivedGiftRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        // public async Task<IEnumerable<ArchivedGift>> AllAsyncWithConnectedData()
        // {
        //     var archivedGifts = RepoDbSet
        //         .Include(a => a.ActionType)
        //         .Include(a => a.Gift)
        //         .Include(a => a.Status)
        //         .Include(a => a.UserGiver)
        //         .Include(a => a.UserReceiver);
        //     return await archivedGifts.ToListAsync();
        // }
        //
        // public async Task<ArchivedGift> FindAsyncWithConnectedData(params object[] id)
        // {
        //     var archivedGift = await base.FindAsync(id);
        //     if (archivedGift.Equals(null))
        //     {
        //         return archivedGift;
        //     }
        //     // NB: Every load causes additional 'round trip' to the database... TODO: Is there a better way to do this?
        //     // TODO: Can't use lamba as param in .Reference() because entity name is not recognized - why?
        //     await RepoDbContext.Entry(archivedGift).Reference("ActionType").LoadAsync();
        //     await RepoDbContext.Entry(archivedGift).Reference("Gift").LoadAsync();
        //     await RepoDbContext.Entry(archivedGift).Reference("Status").LoadAsync();
        //     await RepoDbContext.Entry(archivedGift).Reference("UserGiver").LoadAsync();
        //     await RepoDbContext.Entry(archivedGift).Reference("UserReceiver").LoadAsync();
        //     return archivedGift;
        // }
        //
        // public Task<IEnumerable<ActionType>> GetArchivedGiftActionType()
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public Task<IEnumerable<Gift>> GetArchivedGiftGift()
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public Task<IEnumerable<Status>> GetArchivedGiftStatus()
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public Task<IEnumerable<AppUser>> GetArchivedGiftGiverUser()
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public Task<IEnumerable<AppUser>> GetArchivedGiftReceiverUser()
        // {
        //     throw new NotImplementedException();
        // }
    }
}
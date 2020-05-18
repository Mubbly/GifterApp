using com.mubbly.gifterapp.DAL.Base.EF.Repositories;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DomainApp = Domain.App;
using DALAppDTO = DAL.App.DTO;
using DomainAppIdentity = Domain.App.Identity;

namespace DAL.App.EF.Repositories
{
    public class PrivateMessageRepository :
        EFBaseRepository<AppDbContext, DomainAppIdentity.AppUser, DomainApp.PrivateMessage, DALAppDTO.PrivateMessageDAL>,
        IPrivateMessageRepository
    {
        public PrivateMessageRepository(AppDbContext dbContext) :
            base(dbContext, new DALMapper<DomainApp.PrivateMessage, DALAppDTO.PrivateMessageDAL>())
        {
        }

        // public async Task<IEnumerable<PrivateMessage>> AllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(pm => pm.UserReceiver)
        //         .Include(pm => pm.UserSender)
        //         .AsQueryable();
        //     if (userId != null)
        //     {
        //         // See both sent and received messages
        //         query = query.Where(pm => pm.UserReceiverId == userId || pm.UserSenderId == userId);
        //     }
        //     return await query.ToListAsync();
        // }
        //
        // public async Task<PrivateMessage> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(pm => pm.UserReceiver)
        //         .Include(pm => pm.UserSender)
        //         .Where(pm => pm.Id == id)
        //         .AsQueryable();
        //     if (userId != null)
        //     {
        //         // See both sent and received messages
        //         query = query.Where(pm => pm.UserReceiverId == userId || pm.UserSenderId == userId);
        //     }
        //     return await query.FirstOrDefaultAsync();
        // }
        //
        // public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        // {
        //     if (userId != null)
        //     {
        //         return await RepoDbSet.AnyAsync(pm => pm.Id == id && (pm.UserReceiverId == userId || pm.UserSenderId == userId));
        //     }
        //     return await RepoDbSet.AnyAsync(pm => pm.Id == id);
        // }
        //
        // public async Task DeleteAsync(Guid id, Guid? userId = null)
        // {
        //     var privateMessage = await FirstOrDefaultAsync(id, userId);
        //     base.Remove(privateMessage);
        // }
        //
        // public async Task<IEnumerable<PrivateMessageDTO>> DTOAllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(pm => pm.UserReceiver)
        //         .Include(pm => pm.UserSender)
        //         .AsQueryable();
        //     if (userId != null)
        //     {
        //         // See both sent and received messages
        //         query = query.Where(pm => pm.UserReceiverId == userId || pm.UserSenderId == userId);
        //     }
        //     return await query
        //         .Select(pm => new PrivateMessageDTO() 
        //         {
        //             Id = pm.Id,
        //             Message = pm.Message,
        //             IsSeen = pm.IsSeen,
        //             SentAt = pm.SentAt,
        //             UserReceiverId = pm.UserReceiverId,
        //             UserSenderId = pm.UserSenderId
        //         }).ToListAsync();
        // }
        //
        // public async Task<PrivateMessageDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(pm => pm.UserReceiver)
        //         .Include(pm => pm.UserSender)
        //         .Where(pm => pm.Id == id)
        //         .AsQueryable();
        //     if (userId != null)
        //     {
        //         // See both sent and received messages
        //         query = query.Where(pm => pm.UserReceiverId == userId || pm.UserSenderId == userId);
        //     }
        //     return await query
        //         .Select(pm => new PrivateMessageDTO() 
        //         {
        //             Id = pm.Id,
        //             Message = pm.Message,
        //             IsSeen = pm.IsSeen,
        //             SentAt = pm.SentAt,
        //             UserReceiverId = pm.UserReceiverId,
        //             UserSenderId = pm.UserSenderId
        //         }).FirstOrDefaultAsync();
        // }
    }
}
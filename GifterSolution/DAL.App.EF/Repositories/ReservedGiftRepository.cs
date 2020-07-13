using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.mubbly.gifterapp.DAL.Base.EF.Repositories;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using Microsoft.EntityFrameworkCore;
using DomainApp = Domain.App;
using DALAppDTO = DAL.App.DTO;
using DomainAppIdentity = Domain.App.Identity;

namespace DAL.App.EF.Repositories
{
    public class ReservedGiftRepository :
        EFBaseRepository<AppDbContext, DomainAppIdentity.AppUser, DomainApp.ReservedGift, DALAppDTO.ReservedGiftDAL>,
        IReservedGiftRepository
    {
        public ReservedGiftRepository(AppDbContext dbContext) :
            base(dbContext, new DALMapper<DomainApp.ReservedGift, DALAppDTO.ReservedGiftDAL>())
        {
        }

        /**
         * Get all ReservedGifts - only which are reserved by current user.
         */
        public async Task<IEnumerable<DALAppDTO.ReservedGiftDAL>> GetAllForUserAsync(Guid userId, bool noTracking = true)
        {
            var reservedGifts = PrepareQuery(userId, noTracking);
            var personalReservedGifts =
                await reservedGifts
                    .Where(r => r.UserGiverId == userId)
                    .Include(r => r.UserReceiver)
                    .OrderBy(r => r.CreatedAt)
                    .Select(r => Mapper.Map(r))
                    .ToListAsync();
            return personalReservedGifts;
        }

        /**
         * Get a ReservedGift based on giftId - only if it is reserved by current user.
         */
        public async Task<DALAppDTO.ReservedGiftDAL> GetByGiftId(Guid giftId, Guid? reserverUserId = null, bool noTracking = true)
        {
            var query = PrepareQuery(reserverUserId, noTracking);

            var reservedGift = await query
                .Where(r => r.GiftId == giftId)
                .Where(r => r.UserGiverId == reserverUserId)
                .OrderBy(r => r.CreatedAt)
                // .Include(r => r.Gift)
                // .ThenInclude(g => g != null ? g.Wishlist : null)
                // .ThenInclude(w => w != null ? w.AppUser : null) // TODO: Error InvalidOperationException instance of type Gift cannot be tracked blabla
                .FirstOrDefaultAsync();
            
            return Mapper.Map(reservedGift);
        }

        // public async Task<IEnumerable<ReservedGift>> AllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(rg => rg.ActionType)
        //         .Include(rg => rg.Status)
        //         .Include(rg => rg.UserGiver)
        //         .Include(rg => rg.UserReceiver)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // Only see gifts you have reserved
        //         query = query.Where(rg => rg.UserGiverId == userId);
        //     }
        //
        //     return await query.ToListAsync();
        // }
        //
        // public async Task<ReservedGift> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(rg => rg.ActionType)
        //         .Include(rg => rg.Status)
        //         .Include(rg => rg.UserGiver)
        //         .Include(rg => rg.UserReceiver)
        //         .Where(rg => rg.Id == id)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // Only see gifts you have reserved
        //         query = query.Where(rg => rg.UserGiverId == userId);
        //     }
        //
        //     return await query.FirstOrDefaultAsync();
        // }
        //
        // public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        // {
        //     if (userId != null)
        //     {
        //         return await RepoDbSet.AnyAsync(rg => rg.Id == id && rg.UserGiverId == userId);
        //     }
        //     return await RepoDbSet.AnyAsync(rg => rg.Id == id);
        // }
        //
        // public async Task DeleteAsync(Guid id, Guid? userId = null)
        // {
        //     var reservedGift = await FirstOrDefaultAsync(id, userId);
        //     base.Remove(reservedGift);
        // }
        //
        // public async Task<IEnumerable<ReservedGiftDTO>> DTOAllAsync(Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(rg => rg.ActionType)
        //         .Include(rg => rg.Status)
        //         .Include(rg => rg.UserGiver)
        //         .Include(rg => rg.UserReceiver)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // Only see gifts you have reserved
        //         query = query.Where(rg => rg.UserGiverId == userId);
        //     }
        //
        //     return await query
        //         .Select(rg => new ReservedGiftDTO() 
        //         {
        //             Id = rg.Id,
        //             Comment = rg.Comment,
        //             ReservedFrom = rg.ReservedFrom,
        //             GiftId = rg.GiftId,
        //             StatusId = rg.StatusId,
        //             ActionTypeId = rg.ActionTypeId,
        //             UserGiverId = rg.UserGiverId,
        //             UserReceiverId = rg.UserReceiverId
        //         }).ToListAsync();
        // }
        //
        // public async Task<ReservedGiftDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        // {
        //     var query = RepoDbSet
        //         .Include(rg => rg.ActionType)
        //         .Include(rg => rg.Status)
        //         .Include(rg => rg.UserGiver)
        //         .Include(rg => rg.UserReceiver)
        //         .Where(rg => rg.Id == id)
        //         .AsQueryable();
        //
        //     if (userId != null)
        //     {
        //         // Only see gifts you have reserved
        //         query = query.Where(rg => rg.UserGiverId == userId);
        //     }
        //
        //     return await query
        //         .Select(rg => new ReservedGiftDTO() 
        //         {
        //             Id = rg.Id,
        //             Comment = rg.Comment,
        //             ReservedFrom = rg.ReservedFrom,
        //             GiftId = rg.GiftId,
        //             StatusId = rg.StatusId,
        //             ActionTypeId = rg.ActionTypeId,
        //             UserGiverId = rg.UserGiverId,
        //             UserReceiverId = rg.UserReceiverId
        //         }).FirstOrDefaultAsync();
        // }
    }
}
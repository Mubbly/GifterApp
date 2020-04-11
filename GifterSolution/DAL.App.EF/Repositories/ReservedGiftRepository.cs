using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class ReservedGiftRepository : EFBaseRepository<ReservedGift, AppDbContext>, IReservedGiftRepository
    {
        public ReservedGiftRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<ReservedGift>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(rg => rg.ActionType)
                .Include(rg => rg.Status)
                .Include(rg => rg.UserGiver)
                .Include(rg => rg.UserReceiver)
                .AsQueryable();

            if (userId != null)
            {
                // Only see gifts you have reserved
                query = query.Where(rg => rg.UserGiverId == userId);
            }

            return await query.ToListAsync();
        }

        public async Task<ReservedGift> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(rg => rg.ActionType)
                .Include(rg => rg.Status)
                .Include(rg => rg.UserGiver)
                .Include(rg => rg.UserReceiver)
                .Where(rg => rg.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                // Only see gifts you have reserved
                query = query.Where(rg => rg.UserGiverId == userId);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId != null)
            {
                return await RepoDbSet.AnyAsync(rg => rg.Id == id && rg.UserGiverId == userId);
            }
            return await RepoDbSet.AnyAsync(rg => rg.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var reservedGift = await FirstOrDefaultAsync(id, userId);
            base.Remove(reservedGift);
        }

        public async Task<IEnumerable<ReservedGiftDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(rg => rg.ActionType)
                .Include(rg => rg.Status)
                .Include(rg => rg.UserGiver)
                .Include(rg => rg.UserReceiver)
                .AsQueryable();

            if (userId != null)
            {
                // Only see gifts you have reserved
                query = query.Where(rg => rg.UserGiverId == userId);
            }

            return await query
                .Select(rg => new ReservedGiftDTO() 
                {
                    Id = rg.Id,
                    Comment = rg.Comment,
                    ReservedFrom = rg.ReservedFrom,
                    GiftId = rg.GiftId,
                    StatusId = rg.StatusId,
                    ActionTypeId = rg.ActionTypeId,
                    UserGiverId = rg.UserGiverId,
                    UserReceiverId = rg.UserReceiverId
                }).ToListAsync();
        }

        public async Task<ReservedGiftDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(rg => rg.ActionType)
                .Include(rg => rg.Status)
                .Include(rg => rg.UserGiver)
                .Include(rg => rg.UserReceiver)
                .Where(rg => rg.Id == id)
                .AsQueryable();

            if (userId != null)
            {
                // Only see gifts you have reserved
                query = query.Where(rg => rg.UserGiverId == userId);
            }

            return await query
                .Select(rg => new ReservedGiftDTO() 
                {
                    Id = rg.Id,
                    Comment = rg.Comment,
                    ReservedFrom = rg.ReservedFrom,
                    GiftId = rg.GiftId,
                    StatusId = rg.StatusId,
                    ActionTypeId = rg.ActionTypeId,
                    UserGiverId = rg.UserGiverId,
                    UserReceiverId = rg.UserReceiverId
                }).FirstOrDefaultAsync();
        }
    }
}
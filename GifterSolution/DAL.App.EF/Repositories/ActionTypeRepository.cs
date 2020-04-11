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
    public class ActionTypeRepository : EFBaseRepository<ActionType, AppDbContext>, IActionTypeRepository
    {
        public ActionTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        // TODO: User stuff

        public async Task<IEnumerable<ActionType>> AllAsync(Guid? userId = null)
        {
            return await base.AllAsync();
        }

        public async Task<ActionType> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(at => at.Id == id)
                .AsQueryable();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(d => d.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var actionType = await FirstOrDefaultAsync(id, userId);
            base.Remove(actionType);          
        }

        public async Task<IEnumerable<ActionTypeDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();

            return await query
                .Select(at => new ActionTypeDTO() 
                {
                    Id = at.Id,
                    ActionTypeValue = at.ActionTypeValue,
                    Comment = at.Comment,
                    DonateesCount = at.Donatees.Count,
                    GiftsCount = at.Gifts.Count,
                    ArchivedGiftsCount = at.ArchivedGifts.Count,
                    ReservedGiftsCount = at.ReservedGifts.Count,
                }).ToListAsync();
        }

        public async Task<ActionTypeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(at => at.Id == id)
                .AsQueryable();

            return await query.Select(at => new ActionTypeDTO 
            {
                Id = at.Id,
                ActionTypeValue = at.ActionTypeValue,
                Comment = at.Comment,
                DonateesCount = at.Donatees.Count,
                GiftsCount = at.Gifts.Count,
                ArchivedGiftsCount = at.ArchivedGifts.Count,
                ReservedGiftsCount = at.ReservedGifts.Count,
            }).FirstOrDefaultAsync();
        }
    }
}
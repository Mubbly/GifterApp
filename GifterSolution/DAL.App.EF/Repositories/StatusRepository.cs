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
    public class StatusRepository : EFBaseRepository<Status, AppDbContext>, IStatusRepository
    {
        public StatusRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Status>> AllAsync(Guid? userId = null)
        {
            return await base.AllAsync();
        }

        public async Task<Status> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(s => s.Id == id)
                .AsQueryable();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(s => s.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var status = await FirstOrDefaultAsync(id, userId);
            base.Remove(status);
        }

        public async Task<IEnumerable<StatusDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            return await query
                .Select(s => new StatusDTO()
                {
                    Id = s.Id,
                    StatusValue = s.StatusValue,
                    Comment = s.Comment,
                    DonateesCount = s.Donatees.Count,
                    GiftsCount = s.Gifts.Count,
                    ArchivedGiftsCount = s.ArchivedGifts.Count,
                    ReservedGiftsCount = s.ReservedGifts.Count
                }).ToListAsync();
        }

        public async Task<StatusDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Where(s => s.Id == id)
                .AsQueryable();
            return await query
                .Select(s => new StatusDTO()
                {
                    Id = s.Id,
                    StatusValue = s.StatusValue,
                    Comment = s.Comment,
                    DonateesCount = s.Donatees.Count,
                    GiftsCount = s.Gifts.Count,
                    ArchivedGiftsCount = s.ArchivedGifts.Count,
                    ReservedGiftsCount = s.ReservedGifts.Count
                }).FirstOrDefaultAsync();
        }
    }
}
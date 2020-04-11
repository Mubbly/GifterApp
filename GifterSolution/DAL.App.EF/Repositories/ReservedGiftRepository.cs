using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class ReservedGiftRepository : EFBaseRepository<ReservedGift, AppDbContext>, IReservedGiftRepository
    {
        public ReservedGiftRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public Task<IEnumerable<ReservedGift>> AllAsync(Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<ReservedGift> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id, Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ReservedGiftDTO>> DTOAllAsync(Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<ReservedGiftDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class StatusRepository : EFBaseRepository<Status, AppDbContext>, IStatusRepository
    {
        public StatusRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public Task<IEnumerable<Status>> AllAsync(Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<Status> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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

        public Task<IEnumerable<StatusDTO>> DTOAllAsync(Guid? userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<StatusDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            throw new NotImplementedException();
        }
    }
}
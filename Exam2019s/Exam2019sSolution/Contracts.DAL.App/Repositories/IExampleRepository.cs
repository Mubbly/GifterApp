using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IExampleRepository : IBaseRepository<ExampleDAL>, IExampleRepositoryCustom
    {
        Task<IEnumerable<ExampleDAL>> GetAllWithUserDataAsync(object? userId = null, bool noTracking = true);
        Task<ExampleDAL> GetWithUserDataAsync(Guid id, object? userId = null, bool noTracking = true);
        
        Task<IEnumerable<ExampleDAL>> GetAllForUserAsync(Guid userId, bool noTracking = true);
        Task<ExampleDAL> GetForUserAsync(Guid id, Guid userId, bool noTracking = true);
    }
}
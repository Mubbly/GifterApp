using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using com.mubbly.gifterapp.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IExampleService : IBaseEntityService<ExampleBLL>, IExampleRepositoryCustom<ExampleBLL>
    {
        Task<IEnumerable<ExampleBLL>> GetAllPersonalAsync(Guid userId, bool noTracking = true);
        Task<ExampleBLL> GetPersonalAsync(Guid id, Guid userId, bool noTracking = true);
        Task<IEnumerable<ExampleBLL>> GetAllWithUserDataAsync(Guid userId, bool noTracking = true);
        Task<ExampleBLL> GetWithUserDataAsync(Guid id, Guid userId, bool noTracking = true);
        
        new ExampleBLL Add(ExampleBLL exampleBLL, object? userId = null);
        new Task<ExampleBLL> UpdateAsync(ExampleBLL entity, object? userId = null);
    }
}
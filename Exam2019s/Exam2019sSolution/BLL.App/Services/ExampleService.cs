using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using com.mubbly.gifterapp.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;

namespace BLL.App.Services
{
    public class ExampleService : BaseEntityService<IAppUnitOfWork,
            IExampleRepository, IExampleServiceMapper, ExampleDAL, ExampleBLL>,
        IExampleService
    {
        public ExampleService(IAppUnitOfWork uow) : base(uow, uow.Examples, new ExampleServiceMapper())
        {
        }
        
        public async Task<IEnumerable<ExampleBLL>> GetAllPersonalAsync(Guid userId, bool noTracking = true)
        {
            var personalExamples = (await UOW.Examples.GetAllForUserAsync(userId, noTracking))
                .Select(e => Mapper.Map(e))
                .ToList();
            return personalExamples;
        }
        
        public async Task<ExampleBLL> GetPersonalAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var personalExample = await UOW.Examples.GetForUserAsync(id, userId, noTracking);
            return Mapper.Map(personalExample);
        }
        
        public async Task<IEnumerable<ExampleBLL>> GetAllWithUserDataAsync(Guid userId, bool noTracking = true)
        {
            var personalExamples = (await UOW.Examples.GetAllWithUserDataAsync(userId, noTracking))
                .Select(e => Mapper.Map(e))
                .ToList();
            return personalExamples;
        }
        
        public async Task<ExampleBLL> GetWithUserDataAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var personalExample = await UOW.Examples.GetWithUserDataAsync(id, userId, noTracking);
            return Mapper.Map(personalExample);
        }

        /** userId is required. Include user id */
        public new ExampleBLL Add(ExampleBLL exampleBLL, object? userId = null)
        {
            // UserId is mandatory for adding Examples
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            exampleBLL.AppUserId = new Guid(userId.ToString());
            return base.Add(exampleBLL);
        }

        /** userId is required. Includes user id */
        public new async Task<ExampleBLL> UpdateAsync(ExampleBLL entity, object? userId = null)
        {
            // UserId is mandatory for updating Examples
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            entity.AppUserId = new Guid(userId.ToString());
            return await base.UpdateAsync(entity);
        }

    }
}
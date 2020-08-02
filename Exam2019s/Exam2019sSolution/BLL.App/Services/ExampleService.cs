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
        public ExampleService(IAppUnitOfWork uow) : base(uow, uow.Example, new ExampleServiceMapper())
        {
        }
        
    }
}
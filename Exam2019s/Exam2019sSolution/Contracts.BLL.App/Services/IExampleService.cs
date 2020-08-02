using BLL.App.DTO;
using com.mubbly.gifterapp.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IExampleService : IBaseEntityService<ExampleBLL>, IExampleRepositoryCustom<ExampleBLL>
    {
        
    }
}
using com.mubbly.gifterapp.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IExampleRepository : IBaseRepository<ExampleDAL>, IExampleRepositoryCustom
    {
        
    }
}
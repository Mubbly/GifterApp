using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IExampleRepositoryCustom : IExampleRepositoryCustom<ExampleDAL>
    {
    }

    public interface IExampleRepositoryCustom<TExample>
    {
    }
}
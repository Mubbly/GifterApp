using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IStatusRepositoryCustom : IStatusRepositoryCustom<DALAppDTO.Status>
    {
    }

    public interface IStatusRepositoryCustom<TStatus>
    {
    }
}
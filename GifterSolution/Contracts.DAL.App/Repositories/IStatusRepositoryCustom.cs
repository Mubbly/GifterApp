using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IStatusRepositoryCustom : IStatusRepositoryCustom<DALAppDTO.StatusDAL>
    {
    }

    public interface IStatusRepositoryCustom<TStatus>
    {
    }
}
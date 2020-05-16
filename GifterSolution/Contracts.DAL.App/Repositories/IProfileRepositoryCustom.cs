using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IProfileRepositoryCustom : IProfileRepositoryCustom<DALAppDTO.ProfileDAL>
    {
    }

    public interface IProfileRepositoryCustom<TProfile>
    {
    }
}
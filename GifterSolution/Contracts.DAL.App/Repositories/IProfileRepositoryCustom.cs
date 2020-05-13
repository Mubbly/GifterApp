using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IProfileRepositoryCustom : IProfileRepositoryCustom<DALAppDTO.Profile>
    {
    }

    public interface IProfileRepositoryCustom<TProfile>
    {
    }
}
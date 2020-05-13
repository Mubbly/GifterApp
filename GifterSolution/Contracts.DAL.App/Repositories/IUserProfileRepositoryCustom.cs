using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserProfileRepositoryCustom : IUserProfileRepositoryCustom<DALAppDTO.UserProfile>
    {
    }

    public interface IUserProfileRepositoryCustom<TUserProfile>
    {
    }
}
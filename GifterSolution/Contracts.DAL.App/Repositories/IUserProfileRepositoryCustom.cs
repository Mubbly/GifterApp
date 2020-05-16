using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserProfileRepositoryCustom : IUserProfileRepositoryCustom<DALAppDTO.UserProfileDAL>
    {
    }

    public interface IUserProfileRepositoryCustom<TUserProfile>
    {
    }
}
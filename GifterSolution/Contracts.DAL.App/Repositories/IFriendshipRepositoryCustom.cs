using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IFriendshipRepositoryCustom : IFriendshipRepositoryCustom<DALAppDTO.FriendshipDAL>
    {
    }

    public interface IFriendshipRepositoryCustom<TFriendship>
    {
    }
}
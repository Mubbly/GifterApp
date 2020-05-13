using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IFriendshipRepositoryCustom : IFriendshipRepositoryCustom<DALAppDTO.Friendship>
    {
    }

    public interface IFriendshipRepositoryCustom<TFriendship>
    {
    }
}
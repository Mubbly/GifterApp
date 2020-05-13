using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPrivateMessageRepositoryCustom : IPrivateMessageRepositoryCustom<DALAppDTO.PrivateMessage>
    {
    }

    public interface IPrivateMessageRepositoryCustom<TPrivateMessage>
    {
    }
}
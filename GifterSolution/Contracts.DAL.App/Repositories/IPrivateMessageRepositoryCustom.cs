using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPrivateMessageRepositoryCustom : IPrivateMessageRepositoryCustom<DALAppDTO.PrivateMessageDAL>
    {
    }

    public interface IPrivateMessageRepositoryCustom<TPrivateMessage>
    {
    }
}
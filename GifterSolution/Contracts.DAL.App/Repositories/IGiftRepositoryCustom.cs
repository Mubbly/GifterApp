using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGiftRepositoryCustom : IGiftRepositoryCustom<DALAppDTO.Gift>
    {
    }

    public interface IGiftRepositoryCustom<TGift>
    {
    }
}
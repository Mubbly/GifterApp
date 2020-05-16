using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IGiftRepositoryCustom : IGiftRepositoryCustom<DALAppDTO.GiftDAL>
    {
    }

    public interface IGiftRepositoryCustom<TGift>
    {
    }
}
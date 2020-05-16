using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IReservedGiftRepositoryCustom : IReservedGiftRepositoryCustom<DALAppDTO.ReservedGiftDAL>
    {
    }

    public interface IReservedGiftRepositoryCustom<TReservedGift>
    {
    }
}
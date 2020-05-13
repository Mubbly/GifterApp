using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IReservedGiftRepositoryCustom : IReservedGiftRepositoryCustom<DALAppDTO.ReservedGift>
    {
    }

    public interface IReservedGiftRepositoryCustom<TReservedGift>
    {
    }
}
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IArchivedGiftRepositoryCustom : IArchivedGiftRepositoryCustom<DALAppDTO.ArchivedGiftDAL>
    {
    }

    public interface IArchivedGiftRepositoryCustom<TArchivedGift>
    {
        //Task<IEnumerable<TArchivedGift>> GetAllAsync();
    }
}
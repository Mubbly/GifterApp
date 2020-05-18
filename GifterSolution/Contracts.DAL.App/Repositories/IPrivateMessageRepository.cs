using com.mubbly.gifterapp.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPrivateMessageRepository : IBaseRepository<DALAppDTO.PrivateMessageDAL>,
        IPrivateMessageRepositoryCustom
    {
    }
}
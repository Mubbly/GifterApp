using Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IActionTypeRepository : IBaseRepository<DALAppDTO.ActionType>, IActionTypeRepositoryCustom
    {
    }
}
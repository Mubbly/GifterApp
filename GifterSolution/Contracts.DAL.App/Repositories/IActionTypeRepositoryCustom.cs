using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IActionTypeRepositoryCustom : IActionTypeRepositoryCustom<DALAppDTO.ActionType>
    {
    }

    public interface IActionTypeRepositoryCustom<TActionType>
    {
    }
}
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IActionTypeRepositoryCustom : IActionTypeRepositoryCustom<DALAppDTO.ActionTypeDAL>
    {
    }

    public interface IActionTypeRepositoryCustom<TActionType>
    {
    }
}
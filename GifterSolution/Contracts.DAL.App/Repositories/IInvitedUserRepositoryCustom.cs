using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IInvitedUserRepositoryCustom : IInvitedUserRepositoryCustom<DALAppDTO.InvitedUserDAL>
    {
    }

    public interface IInvitedUserRepositoryCustom<TInvitedUser>
    {
    }
}
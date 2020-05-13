using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IInvitedUserRepositoryCustom : IInvitedUserRepositoryCustom<DALAppDTO.InvitedUser>
    {
    }

    public interface IInvitedUserRepositoryCustom<TInvitedUser>
    {
    }
}
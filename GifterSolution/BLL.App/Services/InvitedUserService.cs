using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Services
{
    public class InvitedUserService : BaseEntityService<IAppUnitOfWork,
            IInvitedUserRepository, IInvitedUserServiceMapper, DALAppDTO.InvitedUserDAL, BLLAppDTO.InvitedUserBLL>,
        IInvitedUserService
    {
        public InvitedUserService(IAppUnitOfWork uow) : base(uow, uow.InvitedUsers, new InvitedUserServiceMapper())
        {
        }
    }
}
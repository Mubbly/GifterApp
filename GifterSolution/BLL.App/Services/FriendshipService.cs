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
    public class FriendshipService : BaseEntityService<IAppUnitOfWork,
            IFriendshipRepository, IFriendshipServiceMapper, DALAppDTO.Friendship, BLLAppDTO.Friendship>,
        IFriendshipService
    {
        public FriendshipService(IAppUnitOfWork uow) : base(uow, uow.Friendships, new FriendshipServiceMapper())
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using com.mubbly.gifterapp.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DALAppDTO = DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;

namespace BLL.App.Services
{
    public class FriendshipService : BaseEntityService<IAppUnitOfWork,
            IFriendshipRepository, IFriendshipServiceMapper, DALAppDTO.FriendshipDAL, BLLAppDTO.FriendshipBLL>,
        IFriendshipService
    {
        public FriendshipService(IAppUnitOfWork uow) : base(uow, uow.Friendships, new FriendshipServiceMapper())
        {
        }

        public async Task<IEnumerable<BLLAppDTO.FriendshipBLL>> GetAllPersonalAsync(Guid userId, bool isConfirmed = true, bool noTracking = true)
        {
            var personalCampaigns = await Repository.GetAllPersonalAsync(userId, isConfirmed, noTracking);
            return personalCampaigns.Select(e => Mapper.Map(e));
        }
    }
}
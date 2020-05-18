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
    public class GiftService : BaseEntityService<IAppUnitOfWork,
            IGiftRepository, IGiftServiceMapper, DALAppDTO.GiftDAL, BLLAppDTO.GiftBLL>,
        IGiftService
    {
        public GiftService(IAppUnitOfWork uow) : base(uow, uow.Gifts, new GiftServiceMapper())
        {
        }

        public async Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllForUser(Guid userId, bool noTracking = true)
        {
            var userGifts = await Repository.GetAllForUserAsync(userId, noTracking);
            return userGifts.Select(e => Mapper.Map(e));
        }

        public async Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllPersonalAsync(Guid userId, bool noTracking = true)
        {
            return await GetAllForUser(userId, noTracking);
        }
        
        public virtual async Task<BLLAppDTO.GiftBLL> GetPersonalAsync(Guid giftId, Guid userId, bool noTracking = true)
        {
            var allPersonalGifts = await GetAllPersonalAsync(userId, noTracking);
            var personalGift = allPersonalGifts.Where(e => e.Id == giftId);

            return personalGift.FirstOrDefault();
        }
    }
}
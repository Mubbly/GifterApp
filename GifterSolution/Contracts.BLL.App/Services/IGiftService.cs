using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IGiftService : IBaseEntityService<BLLAppDTO.GiftBLL>, IGiftRepositoryCustom<BLLAppDTO.GiftBLL>
    {
        Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllForUser(Guid userId, bool noTracking = true);
        Task<IEnumerable<BLLAppDTO.GiftBLL>> GetAllPersonalAsync(Guid userId, bool noTracking = true);
        Task<BLLAppDTO.GiftBLL> GetPersonalAsync(Guid giftId, Guid userId, bool noTracking = true);
    }
}
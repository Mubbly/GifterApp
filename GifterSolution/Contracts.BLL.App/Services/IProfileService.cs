using System;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IProfileService : IBaseEntityService<BLLAppDTO.ProfileBLL>,
        IProfileRepositoryCustom<BLLAppDTO.ProfileBLL>
    {
        Task<BLLAppDTO.ProfileBLL> GetPersonalAsync(Guid userId, Guid? profileId, bool noTracking = true);    
    }
}
using System;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using PublicApi.DTO.v1.Identity;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IProfileService : IBaseEntityService<BLLAppDTO.ProfileBLL>,
        IProfileRepositoryCustom<BLLAppDTO.ProfileBLL>
    {
        Task<BLLAppDTO.ProfileBLL> GetByUserAsync(Guid userId, Guid? profileId = null, bool noTracking = true);
        Task<BLLAppDTO.ProfileBLL> GetFullByUserAsync(Guid userId, Guid? profileId = null, bool noTracking = true);
        BLLAppDTO.ProfileBLL CreateDefaultProfile(Guid userId);
    }
}
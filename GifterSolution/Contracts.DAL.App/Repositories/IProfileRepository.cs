using System;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IProfileRepository : IBaseRepository<DALAppDTO.ProfileDAL>, IProfileRepositoryCustom
    {
        Task<DALAppDTO.ProfileDAL> GetByUserAsync(Guid userId, Guid? profileId, bool noTracking = true);
    }
}
using com.mubbly.gifterapp.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserProfileRepository : IBaseRepository<DALAppDTO.UserProfileDAL>, IUserProfileRepositoryCustom
    {
    }
}
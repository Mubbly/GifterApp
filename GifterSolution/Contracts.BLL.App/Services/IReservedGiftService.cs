using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IReservedGiftService : IBaseEntityService<BLLAppDTO.ReservedGiftBLL>,
        IReservedGiftRepositoryCustom<BLLAppDTO.ReservedGiftBLL>
    {
    }
}
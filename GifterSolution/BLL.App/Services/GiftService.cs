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
    public class GiftService : BaseEntityService<IAppUnitOfWork,
            IGiftRepository, IGiftServiceMapper, DALAppDTO.GiftDAL, BLLAppDTO.GiftBLL>,
        IGiftService
    {
        public GiftService(IAppUnitOfWork uow) : base(uow, uow.Gifts, new GiftServiceMapper())
        {
        }
    }
}
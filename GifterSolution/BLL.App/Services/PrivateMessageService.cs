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
    public class PrivateMessageService : BaseEntityService<IAppUnitOfWork,
            IPrivateMessageRepository, IPrivateMessageServiceMapper, DALAppDTO.PrivateMessageDAL, BLLAppDTO.PrivateMessageBLL
        >,
        IPrivateMessageService
    {
        public PrivateMessageService(IAppUnitOfWork uow) : base(uow, uow.PrivateMessages,
            new PrivateMessageServiceMapper())
        {
        }
    }
}
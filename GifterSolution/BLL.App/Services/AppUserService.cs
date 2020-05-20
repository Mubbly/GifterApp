// using BLL.App.Mappers;
// using com.mubbly.gifterapp.BLL.Base.Services;
// using Contracts.BLL.App.Mappers;
// using Contracts.BLL.App.Services;
// using Contracts.DAL.App;
// using Contracts.DAL.App.Repositories;
// using DALAppDTO = DAL.App.DTO;
// using BLLAppDTO = BLL.App.DTO;
//
// namespace BLL.App.Services
// {
//     public class AppUserService : BaseEntityService<IAppUnitOfWork,
//             IAppUserRepository, IAppUserServiceMapper, DALAppDTO.AppUserDAL, BLLAppDTO.AppUserBLL>,
//         IAppUserService
//     {
//         public AppUserService(IAppUnitOfWork uow) : base(uow, uow.AppUsers, new AppUserServiceMapper())
//         {
//         }
//     }
// }
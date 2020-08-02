using BLL.App.Services;
using com.mubbly.gifterapp.BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork uow) : base(uow)
        {
        }

        // Example:
        // public IAppUserService AppUser =>
        //     GetService<IAppUserService>(() => new AppUserService(UOW));
        
        public IExampleService Example =>
            GetService<IExampleService>(() => new ExampleService(UOW));
    }
}
using com.mubbly.gifterapp.Contracts.BLL.Base;
using Contracts.BLL.App.Services;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        // Example:
        // public IAppUserService AppUser { get; }
        
        public IExampleService Example { get; }

    }
}
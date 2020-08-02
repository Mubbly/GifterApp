using com.mubbly.gifterapp.Contracts.DAL.Base;
using Contracts.DAL.App.Repositories;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
    {
        // Example:
        // IActionTypeRepository ActionTypes { get; }
        
        IExampleRepository Example { get; }
    }
}
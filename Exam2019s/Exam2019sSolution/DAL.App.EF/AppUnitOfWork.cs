using System;
using com.mubbly.gifterapp.DAL.Base.EF;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }

        // Example:
        // public IActionTypeRepository ActionTypes =>
        //     GetRepository<IActionTypeRepository>(() => new ActionTypeRepository(UOWDbContext));
        public IExampleRepository Example =>
            GetRepository<IExampleRepository>(() => new ExampleRepository(UOWDbContext));
    }
}
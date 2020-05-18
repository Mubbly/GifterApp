using System;
using System.Threading.Tasks;

namespace com.mubbly.gifterapp.Contracts.BLL.Base
{
    public interface IBaseBLL
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();

        TService GetService<TService>(Func<TService> serviceCreationMethod)
            where TService : class;
    }
}
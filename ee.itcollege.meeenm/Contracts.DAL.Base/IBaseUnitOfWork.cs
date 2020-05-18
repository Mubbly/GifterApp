using System;
using System.Threading.Tasks;

namespace com.mubbly.gifterapp.Contracts.DAL.Base
{
    public interface IBaseUnitOfWork
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();

        public TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod)
            where TRepository : class;
    }
}
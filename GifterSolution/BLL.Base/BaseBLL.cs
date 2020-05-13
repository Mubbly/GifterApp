using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base;
using Contracts.DAL.Base;

namespace BLL.Base
{
    public class BaseBLL<TUnitOfWork> : IBaseBLL
        where TUnitOfWork : IBaseUnitOfWork
    {
        private readonly Dictionary<Type, object> _serviceCache = new Dictionary<Type, object>();
        protected readonly TUnitOfWork UOW;

        public BaseBLL(TUnitOfWork uow)
        {
            UOW = uow;
        }

        // Factory method
        public TService GetService<TService>(Func<TService> serviceCreationMethod)
            where TService : class
        {
            if (_serviceCache.TryGetValue(typeof(TService), out var repo)) return (TService) repo;

            var newRepo = serviceCreationMethod();
            _serviceCache.Add(typeof(TService), newRepo);
            return newRepo;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await UOW.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return UOW.SaveChanges();
        }
    }
}
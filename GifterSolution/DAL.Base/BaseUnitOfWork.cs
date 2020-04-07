using System;
using System.Collections.Generic;

namespace DAL.Base
{
    public class BaseUnitOfWork
    {
        private readonly Dictionary<Type, object> _repoCache = new Dictionary<Type, object>();
        
        // Factory method
        protected TRepository GetRepository<TRepository>(Func<TRepository> createRepo)
        {
            if(_repoCache.TryGetValue(typeof(TRepository), out var repo))
            {
                return (TRepository)repo;
            }

            var repo2 = createRepo();
            _repoCache.Add(typeof(TRepository), repo2!);
            return (TRepository)repo2;
        }
    }
}
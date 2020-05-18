using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.DAL.Base;
using com.mubbly.gifterapp.Contracts.Domain;

namespace com.mubbly.gifterapp.DAL.Base
{
    public abstract class BaseUnitOfWork<TKey> : IBaseUnitOfWork, IBaseEntityTracker<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly Dictionary<IDomainEntityId<TKey>, IDomainEntityId<TKey>> _entityTracker =
            new Dictionary<IDomainEntityId<TKey>, IDomainEntityId<TKey>>();

        private readonly Dictionary<Type, object> _repoCache = new Dictionary<Type, object>();

        public abstract Task<int> SaveChangesAsync();
        public abstract int SaveChanges();

        // Factory method
        public TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod)
            where TRepository : class
        {
            if (_repoCache.TryGetValue(typeof(TRepository), out var repo))
            {
                return (TRepository) repo;
            }
            var newRepo = repoCreationMethod()!;
            _repoCache.Add(typeof(TRepository), newRepo);
            return newRepo;
        }
        
        public void AddToEntityTracker(IDomainEntityId<TKey> internalEntity, IDomainEntityId<TKey> externalEntity)
        {
            _entityTracker.Add(internalEntity, externalEntity);
        }

        protected void UpdateTrackedEntities()
        {
            foreach (var (key, value) in _entityTracker)
            {
                value.Id = key.Id;
            }
        }
    }
}
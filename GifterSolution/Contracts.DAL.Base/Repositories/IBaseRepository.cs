using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.DAL.Base.Repositories
{
    public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, Guid>
    where TEntity: class, IDomainEntity<Guid>, new()
    {
        
    }

    public interface IBaseRepository<TEntity, TKey>
        where TEntity : class, IDomainEntity<TKey>, new()
        where TKey : struct, IComparable
    {
        // CRUD methods here
        IEnumerable<TEntity> All();
        Task<IEnumerable<TEntity>> AllAsync();
        TEntity Find(params object[] id);
        Task<TEntity> FindAsync(params object[] id);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Remove(TEntity entity);
        TEntity Remove(params object[] id);
        
        // Data store concept, actually not repository thing. Will be moved to be global during UOW homework
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
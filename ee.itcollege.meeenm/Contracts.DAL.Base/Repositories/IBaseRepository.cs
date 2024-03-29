﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.Domain;

namespace com.mubbly.gifterapp.Contracts.DAL.Base.Repositories
{
    public interface IBaseRepository<TEntity> : IBaseRepository<Guid, TEntity>
    where TEntity: class, IDomainEntityId<Guid>, new()
    {}

    public interface IBaseRepository<in TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IDomainEntityId<TKey>, new()
    {
        // CRUD methods here
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">Limit the result to this user's data (entity.userId == userid)</param>
        /// <param name="noTracking">use AsNotracking if datasource supports it</param>
        /// <returns></returns>
        ///
        Task<IEnumerable<TEntity>> GetAllAsync(object? userId = null, bool noTracking = true);
        Task<TEntity> FirstOrDefaultAsync(TKey id, object? userId = null, bool noTracking = true);
        TEntity Add(TEntity entity, object? userId = null);
        Task<TEntity> UpdateAsync(TEntity entity, object? userId = null);
        Task<TEntity> RemoveAsync(TEntity entity, object? userId = null);
        Task<TEntity> RemoveAsync(TKey id, object? userId = null);

        Task<bool> ExistsAsync(TKey id, object? userId = null);
        // IEnumerable<TEntity> All();
        // Task<IEnumerable<TEntity>> AllAsync();
        // TEntity Find(params object[] id);
        // Task<TEntity> FindAsync(params object[] id);
        // TEntity Add(TEntity entity);
        // TEntity Update(TEntity entity);
        // TEntity Remove(TEntity entity);
        // TEntity Remove(params object[] id);
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.mubbly.gifterapp.Contracts.BLL.Base.Mappers;
using com.mubbly.gifterapp.Contracts.BLL.Base.Services;
using com.mubbly.gifterapp.Contracts.DAL.Base;
using com.mubbly.gifterapp.Contracts.DAL.Base.Repositories;
using com.mubbly.gifterapp.Contracts.Domain;

namespace com.mubbly.gifterapp.BLL.Base.Services
{
    public class BaseEntityService<TUOW, TRepository, TMapper, TDALEntity, TBLLEntity> :
        BaseEntityService<Guid, TUOW, TRepository, TMapper, TDALEntity, TBLLEntity>, IBaseEntityService<TBLLEntity>
        where TUOW : IBaseUnitOfWork, IBaseEntityTracker
        where TRepository : IBaseRepository<Guid, TDALEntity>
        where TMapper : IBaseMapper<TDALEntity, TBLLEntity>
        where TDALEntity : class, IDomainEntityId<Guid>, new()
        where TBLLEntity : class, IDomainEntityId<Guid>, new()
    {
        public BaseEntityService(TUOW uow, TRepository repository,
            TMapper mapper) : base(uow, repository, mapper)
        {
        }
    }

    public class
        BaseEntityService<TKey, TUOW, TRepository, TMapper, TDALEntity, TBLLEntity> : IBaseEntityService<TKey,
            TBLLEntity>
        where TKey : IEquatable<TKey>
        where TUOW : IBaseUnitOfWork, IBaseEntityTracker<TKey>
        where TRepository : IBaseRepository<TKey, TDALEntity>
        where TMapper : IBaseMapper<TDALEntity, TBLLEntity>
        where TDALEntity : class, IDomainEntityId<TKey>, new()
        where TBLLEntity : class, IDomainEntityId<TKey>, new()
    {
        // ReSharper disable MemberCanBePrivate.Global
        protected readonly TUOW UOW;
        protected readonly TRepository Repository;

        protected readonly TMapper Mapper;
        // ReSharper enable MemberCanBePrivate.Global

        // ReSharper disable once MemberCanBeProtected.Global
        public BaseEntityService(TUOW uow, TRepository repository, TMapper mapper)
        {
            UOW = uow;
            Repository = repository;
            Mapper = mapper;
        }

        public virtual async Task<IEnumerable<TBLLEntity>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var dalEntities = await Repository.GetAllAsync(userId, noTracking);
            var result = dalEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<TBLLEntity> FirstOrDefaultAsync(TKey id, object? userId = null,
            bool noTracking = true)
        {
            var dalEntity = await Repository.FirstOrDefaultAsync(id, userId, noTracking);
            var result = Mapper.Map(dalEntity);
            return result;
        }

        public TBLLEntity Add(TBLLEntity entity, object? userId = null)
        {
            var dalEntity = Mapper.Map(entity);
            var trackedDALEntity = Repository.Add(dalEntity);
            UOW.AddToEntityTracker(trackedDALEntity, entity);
            var result = Mapper.Map(trackedDALEntity);

            return result;
        }

        public virtual async Task<TBLLEntity> UpdateAsync(TBLLEntity entity, object? userId = null)
        {
            var dalEntity = Mapper.Map(entity);
            var resultDALEntity = await Repository.UpdateAsync(dalEntity, userId);
            var result = Mapper.Map(resultDALEntity);
            return result;
        }

        public virtual async Task<TBLLEntity> RemoveAsync(TBLLEntity entity, object? userId = null)
        {
            var dalEntity = Mapper.Map(entity);
            var resultDALEntity = await Repository.RemoveAsync(dalEntity, userId);
            var result = Mapper.Map(resultDALEntity);
            return result;
        }

        public virtual async Task<TBLLEntity> RemoveAsync(TKey id, object? userId = null)
        {
            var resultDALEntity = await Repository.RemoveAsync(id, userId);
            var result = Mapper.Map(resultDALEntity);
            return result;
        }

        public virtual async Task<bool> ExistsAsync(TKey id, object? userId = null)
        {
            var result = await Repository.ExistsAsync(id, userId);
            return result;
        }
    }
}

// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Contracts.BLL.Base.Mappers;
// using Contracts.BLL.Base.Services;
// using Contracts.DAL.Base;
// using Contracts.DAL.Base.Repositories;
//
// namespace BLL.Base.Services
// {
//     public class BaseEntityService<TServiceRepository, TUnitOfWork, TBLLEntity, TDALEntity> : BaseService, IBaseEntityService<TBLLEntity>
//         where TServiceRepository : IBaseRepository<TDALEntity>
//         where TUnitOfWork : IBaseUnitOfWork
//         where TBLLEntity : class, IDomainBaseEntity, new()
//         where TDALEntity : class, IDomainBaseEntity, new()
//     {
//     protected readonly TUnitOfWork ServiceUnitOfWork;
//     protected readonly IBaseBLLMapper<TDALEntity, TBLLEntity> Mapper;
//     protected readonly TServiceRepository ServiceRepository;
//
//     public BaseEntityService(TUnitOfWork unitOfWork, IBaseBLLMapper<TDALEntity, TBLLEntity> mapper, TServiceRepository serviceRepository)
//     {
//         ServiceUnitOfWork = unitOfWork;
//         ServiceRepository = serviceRepository;
//         Mapper = mapper;
//
//         // TODO - NOT POSSIBLE - we have no idea of what DAL actually is.
//         // we have now BaseRepository implementation - cant call new on it
//         // or ask for func createRepo to create the correct repo
//         //ServiceRepository = ServiceUnitOfWork.GetRepository<IBaseRepository<TDALEntity>>(methodToCreateRepo);
//      }
//
//     public virtual IEnumerable<TBLLEntity> All() =>
//         ServiceRepository.All().Select(entity => Mapper.Map<TDALEntity, TBLLEntity>(entity));
//
//     public virtual async Task<IEnumerable<TBLLEntity>> AllAsync() =>
//         (await ServiceRepository.AllAsync()).Select(entity => Mapper.Map<TDALEntity, TBLLEntity>(entity));
//
//     public virtual TBLLEntity Find(params object[] id) =>
//         Mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Find(id));
//
//     public virtual async Task<TBLLEntity> FindAsync(params object[] id) =>
//         Mapper.Map<TDALEntity, TBLLEntity>(await ServiceRepository.FindAsync(id));
//
//     public virtual TBLLEntity Add(TBLLEntity entity) =>
//         Mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Add(Mapper.Map<TBLLEntity, TDALEntity>(entity)));
//
//     public virtual TBLLEntity Update(TBLLEntity entity) =>
//         Mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Update(Mapper.Map<TBLLEntity, TDALEntity>(entity)));
//
//     public virtual TBLLEntity Remove(TBLLEntity entity) =>
//         Mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Remove(Mapper.Map<TBLLEntity, TDALEntity>(entity)));
//
//     public virtual TBLLEntity Remove(params object[] id) =>
//         Mapper.Map<TDALEntity, TBLLEntity>(ServiceRepository.Remove(id));
//     }
// }
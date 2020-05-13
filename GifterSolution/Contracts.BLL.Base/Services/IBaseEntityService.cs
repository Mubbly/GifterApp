using System;
using Contracts.DAL.Base.Repositories;
using Contracts.Domain;

namespace Contracts.BLL.Base.Services
{
    public interface IBaseEntityService<TBLLEntity> : IBaseEntityService<Guid, TBLLEntity>
        where TBLLEntity : class, IDomainEntityId<Guid>, new()
    {
    }

    public interface IBaseEntityService<in TKey, TBLLEntity> : IBaseService, IBaseRepository<TKey, TBLLEntity>
        where TKey : IEquatable<TKey>
        where TBLLEntity : class, IDomainEntityId<TKey>, new()
    {
        // Automatically implement repository methods
    }
}
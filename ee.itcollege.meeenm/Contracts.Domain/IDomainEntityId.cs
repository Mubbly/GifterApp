using System;

namespace ee.itcollege.meeenm.Contracts.Domain
{
    public interface IDomainEntityId : IDomainEntityId<Guid>
    {
    }

    public interface IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}
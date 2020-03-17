using System;

namespace Contracts.DAL.Base
{
    // MSSQL supports Guid ootb
    public interface IDomainEntity : IDomainEntity<Guid>
    {
        
    }

    public interface IDomainEntity<TKey> : IDomainBaseEntity<TKey>, IDomainEntityMetadata
    where TKey: struct, IComparable
    {
        
    }
}
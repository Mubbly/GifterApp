using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class UserPermission : UserPermission<Guid>, IDomainEntity
    {
        
    }
    
    /**
     * Temporary table - will actually preexist from Identity
     */
    public class UserPermission<TKey> : DomainEntity<TKey>
        where TKey: struct, IEquatable<TKey>
    {
        public virtual DateTime From { get; set; } = default!;
        public virtual DateTime To { get; set; } = default!;
        [MaxLength(2048)] [MinLength(3)] 
        public virtual string? Comment { get; set; }

        public virtual TKey AppUserId { get; set; } = default!;
        public virtual AppUser? AppUser { get; set; }

        public virtual TKey PermissionId { get; set; } = default!;
        public virtual Permission? Permission { get; set; }

    }
}
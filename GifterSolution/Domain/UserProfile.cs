using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class UserProfile : UserProfile<Guid>, IDomainEntity
    {
        
    }
    
    public class UserProfile<TKey> : DomainEntity<TKey>
        where TKey: struct, IEquatable<TKey>
    {
        [MaxLength(2048)] [MinLength(3)] 
        public virtual string? Comment { get; set; }

        public virtual TKey AppUserId { get; set; } = default!;
        public virtual AppUser? AppUser { get; set; }

        public virtual TKey ProfileId { get; set; } = default!;
        public virtual Profile? Profile { get; set; }
    }
}
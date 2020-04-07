using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Friendship : Friendship<Guid>, IDomainEntity
    {
        
    }
    
    /**
     * Users can add other users to their friendlist
     * Friends have more permissions (ex. view profile when it's private, send private messages) 
     */
    public class Friendship<TKey> : DomainEntity<TKey>
        where TKey: struct, IEquatable<TKey>
    {
        public virtual bool IsConfirmed { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public virtual string? Comment { get; set; }

        // Requester
        [ForeignKey(nameof(AppUser1))]
        public virtual TKey AppUser1Id { get; set; } = default!;
        public virtual AppUser? AppUser1 { get; set; }
        
        // Addressee
        [ForeignKey(nameof(AppUser2))]
        public virtual TKey AppUser2Id { get; set; } = default!;
        public virtual AppUser? AppUser2 { get; set; }
    }
}
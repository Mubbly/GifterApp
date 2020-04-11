using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Gift : Gift<Guid>, IDomainEntity
    {
        
    }
    
    /**
     * Gift is an item that the user can add to their Wishlist on their Profile
     * Other users can see and interact with it
     */
    public class Gift<TKey> : DomainEntity<TKey>
        where TKey: struct, IEquatable<TKey>
    {
        [MaxLength(256)] [MinLength(1)] 
        public virtual string Name { get; set; } = default!;
        [MaxLength(1024)] [MinLength(3)] 
        public virtual string? Description { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public virtual string? Image { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public virtual string? Url { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public virtual string? PartnerUrl { get; set; }
        public virtual bool IsPartnered { get; set; }
        public virtual bool IsPinned { get; set; }

        public virtual TKey ActionTypeId { get; set; } = default!;
        public virtual ActionType? ActionType { get; set; }

        public virtual TKey StatusId { get; set; } = default!;
        public virtual Status? Status { get; set; }

        public virtual TKey WishlistId { get; set; } = default!;
        public virtual Wishlist? Wishlist { get; set; }
        
        public virtual TKey AppUserId { get; set; } = default!;
        public virtual AppUser? AppUser { get; set; }

        // List of all gifts that have reserved status
        public virtual ICollection<ReservedGift>? ReservedGifts { get; set; }

        // List of all gifts that have archived status
        public virtual ICollection<ArchivedGift>? ArchivedGifts { get; set; }
    }
}
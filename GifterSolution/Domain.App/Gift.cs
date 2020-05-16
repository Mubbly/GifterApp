using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Gift : Gift<Guid>
    {
        
    }
    
    /**
     * Gift is an item that the user can add to their Wishlist on their Profile
     * Other users can see and interact with it
     */
    public class Gift<TKey> : DomainEntityIdMetadata
        where TKey : struct, IEquatable<TKey>
    {
        [MaxLength(256)] [MinLength(1)] public virtual string Name { get; set; } = default!;

        [MaxLength(1024)] [MinLength(3)] public virtual string? Description { get; set; }

        [MaxLength(2048)] [MinLength(3)] public virtual string? Image { get; set; }

        [MaxLength(2048)] [MinLength(3)] public virtual string? Url { get; set; }

        [MaxLength(2048)] [MinLength(3)] public virtual string? PartnerUrl { get; set; }

        public virtual bool IsPartnered { get; set; }
        public virtual bool IsPinned { get; set; }

        public virtual TKey ActionTypeId { get; set; } = default!;
        public virtual ActionType? ActionType { get; set; }

        public virtual TKey StatusId { get; set; } = default!;
        public virtual Status? Status { get; set; }

        public virtual TKey WishlistId { get; set; } = default!;
        public virtual Wishlist? Wishlist { get; set; }
        
        // List of all gifts that have reserved status
        [InverseProperty(nameof(ReservedGift.Gift))]
        public virtual ICollection<ReservedGift>? ReservedGifts { get; set; }

        // List of all gifts that have archived status
        [InverseProperty(nameof(ArchivedGift.Gift))]
        public virtual ICollection<ArchivedGift>? ArchivedGifts { get; set; }
    }
}
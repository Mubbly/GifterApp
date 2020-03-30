using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    /**
     * Gift is an item that the user can add to their Wishlist on their Profile
     * Other users can see and interact with it
     */
    public class Gift : DomainEntity
    {
        [MaxLength(256)] [MinLength(1)] 
        public string Name { get; set; } = default!;
        [MaxLength(1024)] [MinLength(3)] 
        public string? Description { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? Image { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? Url { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? PartnerUrl { get; set; }
        public bool IsPartnered { get; set; }
        public bool IsPinned { get; set; }

        public Guid ActionTypeId { get; set; } = default!;
        public ActionType? ActionType { get; set; }

        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        public Guid StatusId { get; set; } = default!;
        public Status? Status { get; set; }

        // List of all wishlists that correspond to this gift
        public ICollection<Wishlist>? Wishlists { get; set; }

        // List of all gifts that have reserved status
        public ICollection<ReservedGift>? ReservedGifts { get; set; }

        // List of all gifts that have archived status
        public ICollection<ArchivedGift>? ArchivedGifts { get; set; }
    }
}
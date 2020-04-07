using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class ActionType : ActionType<Guid>, IDomainEntity
    {
    }

    /**
     * Specific actions that can be taken with regular and donatee Gifts.
     * Example action values:
     *     "Active" state gift: 1) Reserve
     *     "Reserved" state gift: 1) Gifted/Archive 2) Cancel/Set active
     *     "Archived" state gift: 1) Confirm/Archive 2) Deny/Set active
     */
    public class  ActionType<TKey> : DomainEntity<TKey>
        where TKey: struct, IEquatable<TKey>
    {
        [MaxLength(64)] [MinLength(1)] 
        public virtual string ActionTypeValue { get; set; } = default!;
        [MaxLength(2048)] [MinLength(3)] 
        public virtual string? Comment { get; set; }
        
        // List of all the gifts that correspond to this action
        public virtual ICollection<Gift>? Gifts { get; set; } // = new List<Gift>(); TODO: Should lists be initialized?
        
        // List of all the reserved gifts that correspond to this action
        public virtual ICollection<ReservedGift>? ReservedGifts { get; set; }
        
        // List of all the archived gifts that correspond to this action
        public virtual ICollection<ArchivedGift>? ArchivedGifts { get; set; }
        
        // List of all the donatees that correspond to this action
        public virtual ICollection<Donatee>? Donatees { get; set; }
    }
}
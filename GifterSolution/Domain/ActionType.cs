using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    /**
     * Specific actions that can be taken with regular and donatee Gifts.
     * Example action values:
     *     "Active" state gift: 1) Reserve
     *     "Reserved" state gift: 1) Gifted/Archive 2) Cancel/Set active
     *     "Archived" state gift: 1) Confirm/Archive 2) Deny/Set active
     */
    public class ActionType : DomainEntityMetadata
    {
        [MaxLength(64)] [MinLength(1)] 
        public string ActionTypeValue { get; set; } = default!;
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }
        
        // List of all the gifts that correspond to this action
        public ICollection<Gift>? Gifts { get; set; }
        
        // List of all the reserved gifts that correspond to this action
        public ICollection<ReservedGift>? ReservedGifts { get; set; }
        
        // List of all the archived gifts that correspond to this action
        public ICollection<ArchivedGift>? ArchivedGifts { get; set; }
        
        // List of all the donatees that correspond to this action
        public ICollection<Donatee>? Donatees { get; set; }
    }
}
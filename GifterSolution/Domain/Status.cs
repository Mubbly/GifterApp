using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    /**
     * Status of any regular or donatee Gift.
     * Example values:
     *     Active
     *     Reserved
     *     Archieved
     */    
    public class Status : DomainEntity
    {
        [MaxLength(64)] [MinLength(1)] 
        public string StatusValue { get; set; } = default!;
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }
        
        // List of all gifts that correspond to this status
        public ICollection<Gift>? Gifts { get; set; }

        // List of all the reserved gifts that correspond to this status
        public ICollection<ReservedGift>? ReservedGifts { get; set; }
        
        // List of all the archived gifts that correspond to this status
        public ICollection<ArchivedGift>? ArchivedGifts { get; set; }
        
        // List of all the donatees that correspond to this status
        public ICollection<Donatee>? Donatees { get; set; }
    }
}
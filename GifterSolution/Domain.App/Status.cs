using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using com.mubbly.gifterapp.Domain.Base;

namespace Domain.App
{
    public class Status : Status<Guid>
    {
        
    }
    
    /**
     * Status of any regular or donatee Gift.
     * Example values:
     *     Active
     *     Reserved
     *     Archieved
     */
    public class Status<TKey> : DomainEntityIdMetadata
        where TKey : struct, IEquatable<TKey>
    {
        [MaxLength(64)] [MinLength(1)] public virtual string StatusValue { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public virtual string? Comment { get; set; }

        // List of all gifts that correspond to this status
        [InverseProperty(nameof(Gift.Status))]
        public virtual ICollection<Gift>? Gifts { get; set; }

        // List of all the reserved gifts that correspond to this status
        [InverseProperty(nameof(ReservedGift.Status))]
        public virtual ICollection<ReservedGift>? ReservedGifts { get; set; }

        // List of all the archived gifts that correspond to this status
        [InverseProperty(nameof(ArchivedGift.Status))]
        public virtual ICollection<ArchivedGift>? ArchivedGifts { get; set; }

        // List of all the donatees that correspond to this status
        [InverseProperty(nameof(Donatee.Status))]
        public virtual ICollection<Donatee>? Donatees { get; set; }
    }
}
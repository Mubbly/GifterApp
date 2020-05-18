using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Domain.Base;

namespace Domain.App
{
    public class ActionType : ActionType<Guid>
    {
    }

    /**
     * Specific actions that can be taken with regular and donatee Gifts.
     * Example action values:
     *     "Active" state gift: 1) Reserve
     *     "Reserved" state gift: 1) Gifted/Archive 2) Cancel/Set active
     *     "Archived" state gift: 1) Confirm/Archive 2) Deny/Set active
     */
    public class ActionType<TKey> : DomainEntityIdMetadata
        where TKey : IEquatable<TKey>
    {
        [MaxLength(64)] [MinLength(1)] public virtual string ActionTypeValue { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public virtual string? Comment { get; set; }

        // List of all the gifts that correspond to this action
        [InverseProperty(nameof(Gift.ActionType))]
        public virtual ICollection<Gift>? Gifts { get; set; } // = new List<Gift>(); TODO: Should lists be initialized?

        // List of all the reserved gifts that correspond to this action
        [InverseProperty(nameof(ReservedGift.ActionType))]
        public virtual ICollection<ReservedGift>? ReservedGifts { get; set; }

        // List of all the archived gifts that correspond to this action
        [InverseProperty(nameof(ArchivedGift.ActionType))]
        public virtual ICollection<ArchivedGift>? ArchivedGifts { get; set; }

        // List of all the donatees that correspond to this action
        [InverseProperty(nameof(Donatee.ActionType))]
        public virtual ICollection<Donatee>? Donatees { get; set; }
    }
}
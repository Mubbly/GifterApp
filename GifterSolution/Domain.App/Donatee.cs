using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Donatee : Donatee<Guid>
    {
        
    }
    
    /**
     * Donatee is basically 2in1 UserProfile + Gift
     * These are temporary "person with a wish" tokens made by campaign manager user
     * so that participants wouldn't have to actually register one by one
     */
    public class Donatee<TKey> : DomainEntityIdMetadata
        where TKey : struct, IEquatable<TKey>
    {
        [MaxLength(256)] [MinLength(1)] public virtual string FirstName { get; set; } = default!;

        [MaxLength(256)] [MinLength(1)] public virtual string? LastName { get; set; }

        [MaxLength(256)] [MinLength(1)] public virtual string? Gender { get; set; }

        public virtual int? Age { get; set; }

        [MaxLength(4096)] [MinLength(3)] public virtual string? Bio { get; set; }

        [MaxLength(256)] [MinLength(1)] public virtual string GiftName { get; set; } = default!;

        [MaxLength(1024)] [MinLength(3)] public virtual string? GiftDescription { get; set; }

        [MaxLength(2048)] [MinLength(3)] public virtual string? GiftImage { get; set; }

        [MaxLength(2048)] [MinLength(3)] public virtual string? GiftUrl { get; set; }

        public virtual DateTime? GiftReservedFrom { get; set; }
        public virtual DateTime ActiveFrom { get; set; }
        public virtual DateTime ActiveTo { get; set; }
        public virtual bool IsActive { get; set; }

        public virtual string? FullName => FirstName + " " + LastName;

        public virtual TKey ActionTypeId { get; set; } = default!;
        public virtual ActionType? ActionType { get; set; }

        public virtual TKey StatusId { get; set; } = default!;
        public virtual Status? Status { get; set; }

        [InverseProperty(nameof(CampaignDonatee.Donatee))]
        public virtual ICollection<CampaignDonatee>? DonateeCampaigns { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using com.mubbly.gifterapp.Domain.Base;

namespace Domain.App
{
    public class CampaignDonatee : CampaignDonatee<Guid>
    {
    }

    public class CampaignDonatee<TKey> : DomainEntityIdMetadata
        where TKey : IEquatable<TKey>
    {
        public virtual bool IsActive { get; set; } = default!;

        [MaxLength(1024)] [MinLength(3)] public virtual string? Comment { get; set; }

        public virtual TKey CampaignId { get; set; } = default!;
        public virtual Campaign? Campaign { get; set; }

        public virtual TKey DonateeId { get; set; } = default!;
        public virtual Donatee? Donatee { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{ 
    public class CampaignDonatee : CampaignDonatee<Guid>, IDomainEntity
    {
        
    }
    
    public class CampaignDonatee<TKey> : DomainEntity<TKey>
        where TKey: struct, IEquatable<TKey>
    {
        public virtual bool IsActive { get; set; } = default!;
        [MaxLength(1024)] [MinLength(3)] 
        public virtual string? Comment { get; set; }
        
        public virtual TKey CampaignId { get; set; } = default!;
        public virtual Campaign? Campaign { get; set; }
        
        public virtual TKey DonateeId { get; set; } = default!;
        public virtual Donatee? Donatee { get; set; }
    }
}
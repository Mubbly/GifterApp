using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class CampaignDonatee : DomainEntity
    {
        public bool IsActive { get; set; } = default!;
        [MaxLength(1024)] [MinLength(3)] 
        public string? Comment { get; set; }
        
        public Guid CampaignId { get; set; } = default!;
        public Campaign? Campaign { get; set; }
        
        public Guid DonateeId { get; set; } = default!;
        public Donatee? Donatee { get; set; }
    }
}
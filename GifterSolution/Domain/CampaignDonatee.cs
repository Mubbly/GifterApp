using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class CampaignDonatee : DomainEntityMetadata
    {
        public bool IsActive { get; set; } = default!;
        [MaxLength(1024)] [MinLength(3)] 
        public string? Comment { get; set; }
        
        [MaxLength(36)]
        public string CampaignId { get; set; } = default!;
        public Campaign? Campaign { get; set; }
        
        [MaxLength(36)]
        public string DonateeId { get; set; } = default!;
        public Donatee? Donatee { get; set; }
    }
}
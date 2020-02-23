using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    /**
     * Some users have special rights to create temporary campaigns
     * Each campaign has donatees/people you can donate certain gifts to
     * Example: Children from an orphanage during Christmas holidays
     */
    public class Campaign : DomainEntityMetadata
    {
        [MaxLength(512)] [MinLength(1)] 
        public string Name { get; set; } = default!;
        [MaxLength(4096)] [MinLength(3)] 
        public string? Description { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? AdImage { get; set; } // TODO: Google general sql link length standards
        [MaxLength(512)] [MinLength(1)] 
        public string? Institution { get; set; }
        
        public DateTime ActiveFromDate { get; set; }
        public DateTime ActiveToDate { get; set; }
        public bool IsActive { get; set; }
        
        // List of mapped campaigns and (campaign manager) users
        public ICollection<UserCampaign>? UserCampaigns { get; set; }
        
        // List of mapped campaigns and donatees
        public ICollection<CampaignDonatee>? CampaignDonatees { get; set; }
    }
}
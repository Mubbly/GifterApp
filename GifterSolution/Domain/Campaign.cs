using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{ 
    public class Campaign : Campaign<Guid>, IDomainEntity
    {
        
    }
    
    /**
     * Some users have special rights to create temporary campaigns
     * Each campaign has donatees/people you can donate certain gifts to
     * Example: Children from an orphanage during Christmas holidays
     */
    public class Campaign<TKey> : DomainEntity<TKey>
        where TKey: struct, IEquatable<TKey>
    {
        [MaxLength(512)] [MinLength(1)] 
        public virtual string Name { get; set; } = default!;
        [MaxLength(4096)] [MinLength(3)] 
        public virtual string? Description { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public virtual string? AdImage { get; set; } // TODO: Google general sql link length standards
        [MaxLength(512)] [MinLength(1)] 
        public virtual string? Institution { get; set; }
        
        public virtual DateTime ActiveFromDate { get; set; }
        public virtual DateTime ActiveToDate { get; set; }
        public virtual bool IsActive { get; set; }
        
        // List of mapped campaigns and (campaign manager) users
        public virtual ICollection<UserCampaign>? UserCampaigns { get; set; }
        
        // List of mapped campaigns and donatees
        public virtual ICollection<CampaignDonatee>? CampaignDonatees { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using com.mubbly.gifterapp.Domain.Base;

namespace Domain.App
{ 
    public class Campaign : Campaign<Guid>
    {
        
    }
    
    /**
     * Some users have special rights to create temporary campaigns
     * Each campaign has donatees/people you can donate certain gifts to
     * Example: Children from an orphanage during Christmas holidays
     */
    public class Campaign<TKey> : DomainEntityIdMetadata
        where TKey : IEquatable<TKey>
    {
        [MaxLength(512)] [MinLength(1)] public virtual string Name { get; set; } = default!;

        [MaxLength(4096)] [MinLength(3)] public virtual string? Description { get; set; }

        [MaxLength(2048)]
        [MinLength(3)]
        public virtual string? AdImage { get; set; } // TODO: Google general sql link length standards

        [MaxLength(512)] [MinLength(1)] public virtual string? Institution { get; set; }

        public virtual DateTime ActiveFromDate { get; set; }
        public virtual DateTime ActiveToDate { get; set; }
        public virtual bool IsActive { get; set; }

        // List of mapped campaigns and (campaign manager) users
        [InverseProperty(nameof(UserCampaign.Campaign))]
        public virtual ICollection<UserCampaign>? UserCampaigns { get; set; }

        // List of mapped campaigns and donatees
        [InverseProperty(nameof(CampaignDonatee.Campaign))]
        public virtual ICollection<CampaignDonatee>? CampaignDonatees { get; set; }
    }
}
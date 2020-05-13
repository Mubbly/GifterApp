using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class Campaign : IDomainEntityId
    {
        [MaxLength(512)] [MinLength(1)] public string Name { get; set; } = default!;

        [MaxLength(4096)] [MinLength(3)] public string? Description { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? AdImage { get; set; }

        [MaxLength(512)] [MinLength(1)] public string? Institution { get; set; }

        public virtual DateTime ActiveFromDate { get; set; }
        public virtual DateTime ActiveToDate { get; set; }

        public virtual bool IsActive { get; set; }
        public int UserCampaignsCount { get; set; }
        public int CampaignDonateesCount { get; set; }

        public Guid Id { get; set; }
        // public virtual ICollection<UserCampaign>? UserCampaigns { get; set; }
        // public virtual ICollection<CampaignDonatee>? CampaignDonatees { get; set; }
    }
}
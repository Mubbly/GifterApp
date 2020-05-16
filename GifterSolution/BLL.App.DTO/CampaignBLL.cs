using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class CampaignBLL : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(512)] [MinLength(1)] public string Name { get; set; } = default!;

        [MaxLength(4096)] [MinLength(3)] public string? Description { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? AdImage { get; set; }

        [MaxLength(512)] [MinLength(1)] public string? Institution { get; set; }

        public virtual DateTime ActiveFromDate { get; set; }
        public virtual DateTime ActiveToDate { get; set; }

        public virtual bool IsActive { get; set; }

        // List of mapped campaigns and (campaign manager) users
        [InverseProperty(nameof(UserCampaignBLL.Campaign))]
        public virtual ICollection<UserCampaignBLL>? UserCampaigns { get; set; }

        // List of mapped campaigns and donatees
        [InverseProperty(nameof(CampaignDonateeBLL.Campaign))]
        public virtual ICollection<CampaignDonateeBLL>? CampaignDonatees { get; set; }
    }
}
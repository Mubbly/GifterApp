using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class CampaignDAL : IDomainEntityId
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
        [InverseProperty(nameof(UserCampaignDAL.Campaign))]
        public virtual ICollection<UserCampaignDAL>? UserCampaigns { get; set; }

        // List of mapped campaigns and donatees
        [InverseProperty(nameof(CampaignDonateeDAL.Campaign))]
        public virtual ICollection<CampaignDonateeDAL>? CampaignDonatees { get; set; }
    }
}
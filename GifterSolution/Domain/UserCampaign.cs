using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class UserCampaign : DomainEntity
    {
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }

        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        public Guid CampaignId { get; set; } = default!;
        public Campaign? Campaign { get; set; }
    }
}
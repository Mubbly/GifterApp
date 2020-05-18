using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using com.mubbly.gifterapp.Contracts.Domain;

namespace BLL.App.DTO
{
    public class UserCampaignBLL : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUserBLL AppUser { get; set; } = default!;
        
        public Guid CampaignId { get; set; }
        public CampaignBLL Campaign { get; set; } = default!;
    }
}
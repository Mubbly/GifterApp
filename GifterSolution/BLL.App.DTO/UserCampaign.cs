using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using Domain.App.Identity;

namespace BLL.App.DTO
{
    public class UserCampaign : IDomainEntityId
    {
        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; } = default!;
        public Guid CampaignId { get; set; }
        public Campaign Campaign { get; set; } = default!;
        public Guid Id { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using DAL.App.DTO.Identity;
using Domain.App.Identity;

namespace DAL.App.DTO
{
    public class UserCampaignDAL : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUserDAL AppUser { get; set; } = default!;
        public Guid CampaignId { get; set; }
        public CampaignDAL Campaign { get; set; } = default!;
    }
}
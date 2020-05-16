using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class UserProfileBLL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUserBLL AppUser { get; set; } = default!;
        public Guid ProfileId { get; set; }
        public ProfileBLL Profile { get; set; } = default!;
    }
}
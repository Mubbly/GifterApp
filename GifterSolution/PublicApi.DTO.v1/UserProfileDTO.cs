using System;
using System.ComponentModel.DataAnnotations;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class UserProfileDTO
    {
        public Guid Id { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUserDTO AppUser { get; set; } = default!;
        public Guid ProfileId { get; set; }
        public ProfileDTO Profile { get; set; } = default!;
    }
}
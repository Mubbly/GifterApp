using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class UserProfileDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUsersDTO AppUser { get; set; } = default!;
        public Guid ProfileId { get; set; }
        public ProfileDTO Profile { get; set; } = default!;
    }
}
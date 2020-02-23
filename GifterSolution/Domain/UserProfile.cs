using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class UserProfile : DomainEntityMetadata
    {
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }

        [MaxLength(36)]
        public string AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        [MaxLength(36)]
        public string ProfileId { get; set; } = default!;
        public Profile? Profile { get; set; }
    }
}
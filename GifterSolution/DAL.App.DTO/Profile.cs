using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Profile : IDomainEntityId
    {
        [MaxLength(2048)] [MinLength(3)] public string? ProfilePicture { get; set; }

        [MaxLength(256)] [MinLength(1)] public string? Gender { get; set; }

        [MaxLength(512)] [MinLength(1)] public string? Bio { get; set; }

        public int? Age { get; set; }
        public bool IsPrivate { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; } = default!;
        public Guid WishlistId { get; set; }
        public Wishlist Wishlist { get; set; } = default!;

        public int UserProfilesCount { get; set; }
        public Guid Id { get; set; }
    }
}
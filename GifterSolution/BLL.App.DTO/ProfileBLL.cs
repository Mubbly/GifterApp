using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.App.DTO.Identity;
using com.mubbly.gifterapp.Contracts.Domain;

namespace BLL.App.DTO
{
    public class ProfileBLL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(2048)] [MinLength(3)] public string? ProfilePicture { get; set; }

        [MaxLength(256)] [MinLength(1)] public string? Gender { get; set; }

        [MaxLength(512)] [MinLength(1)] public string? Bio { get; set; }

        public int? Age { get; set; }
        public bool IsPrivate { get; set; }

        public Guid AppUserId { get; set; }
        public AppUserBLL? AppUser { get; set; } = default!;
        
        public Guid WishlistId { get; set; }
        public WishlistBLL? Wishlist { get; set; } = default!;

        // List of mapped users and their profiles
        [InverseProperty(nameof(UserProfileBLL.Profile))]
        public virtual ICollection<UserProfileBLL>? UserProfiles { get; set; }
    }
}
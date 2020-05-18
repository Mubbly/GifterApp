using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class ProfileDAL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(2048)] [MinLength(3)] public string? ProfilePicture { get; set; }

        [MaxLength(256)] [MinLength(1)] public string? Gender { get; set; }

        [MaxLength(512)] [MinLength(1)] public string? Bio { get; set; }

        public int? Age { get; set; }
        public bool IsPrivate { get; set; }

        public Guid AppUserId { get; set; }
        public AppUserDAL AppUser { get; set; } = default!;
        
        public Guid WishlistId { get; set; }
        public WishlistDAL Wishlist { get; set; } = default!;

        // List of mapped users and their profiles
        [InverseProperty(nameof(UserProfileDAL.Profile))]
        public ICollection<UserProfileDAL>? UserProfiles { get; set; }
    }
}
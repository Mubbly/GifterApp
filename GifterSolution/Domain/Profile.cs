using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    /**
     * Every user has a profile where they can share info about themselves
     * And fill in their Wishlist of Gifts
     * Profile can be private (seen to friends) or public (seen to all users)
     */
    public class Profile : DomainEntityMetadata
    {
        [MaxLength(2048)] [MinLength(3)] 
        public string? ProfilePicture { get; set; }
        [MaxLength(256)] [MinLength(1)] 
        public string? Gender { get; set; }
        [MaxLength(512)] [MinLength(1)] 
        public string? Bio { get; set; }
        public int? Age { get; set; }
        public bool IsPrivate { get; set; }

        [MaxLength(36)]
        public string AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
        
        [MaxLength(36)]
        public string WishlistId { get; set; } = default!;
        public Wishlist? Wishlist { get; set; }
        
        // List of mapped users and their profiles
        public ICollection<UserProfile>? UserProfiles { get; set; }
    }
}
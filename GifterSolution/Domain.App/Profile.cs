using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Profile : Profile<Guid>
    {
        
    }
    
    /**
     * Every user has a profile where they can share info about themselves
     * And fill in their Wishlist of Gifts
     * Profile can be private (seen to friends) or public (seen to all users)
     */
    public class Profile<TKey> : DomainEntityIdMetadataUser<AppUser>
        where TKey : struct, IEquatable<TKey>
    {
        [MaxLength(2048)] [MinLength(3)] public virtual string? ProfilePicture { get; set; }

        [MaxLength(256)] [MinLength(1)] public virtual string? Gender { get; set; }

        [MaxLength(512)] [MinLength(1)] public virtual string? Bio { get; set; }

        public virtual int? Age { get; set; }
        public virtual bool IsPrivate { get; set; }

        // AppUser

        public virtual TKey WishlistId { get; set; } = default!; // TODO
        public virtual Wishlist? Wishlist { get; set; }

        // List of mapped users and their profiles
        [InverseProperty(nameof(UserProfile.Profile))]
        public virtual ICollection<UserProfile>? UserProfiles { get; set; }
    }
}
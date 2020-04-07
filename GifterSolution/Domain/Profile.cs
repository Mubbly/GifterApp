using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Profile : Profile<Guid>, IDomainEntity
    {
        
    }
    
    /**
     * Every user has a profile where they can share info about themselves
     * And fill in their Wishlist of Gifts
     * Profile can be private (seen to friends) or public (seen to all users)
     */
    public class Profile<TKey> : DomainEntity<TKey>
        where TKey: struct, IEquatable<TKey>
    {
        [MaxLength(2048)] [MinLength(3)] 
        public virtual string? ProfilePicture { get; set; }
        [MaxLength(256)] [MinLength(1)] 
        public virtual string? Gender { get; set; }
        [MaxLength(512)] [MinLength(1)] 
        public virtual string? Bio { get; set; }
        public virtual int? Age { get; set; }
        public virtual bool IsPrivate { get; set; }

        public virtual TKey AppUserId { get; set; } = default!;
        public virtual AppUser? AppUser { get; set; }
        
        public virtual TKey WishlistId { get; set; } = default!;
        public virtual Wishlist? Wishlist { get; set; }
        
        // List of mapped users and their profiles
        public virtual ICollection<UserProfile>? UserProfiles { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Wishlist : Wishlist<Guid>, IDomainEntity
    {
        
    }

    /**
     * Wishlist is a list of Gifts that a user can create on their Profile
     * It is possible to see and interact with other users' Wishlists
     */
    public class Wishlist<TKey> : DomainEntity<TKey>
        where TKey: struct, IEquatable<TKey>
    {
        [MaxLength(2048)] [MinLength(3)] 
        public virtual string? Comment { get; set; }
        
        // To connect to logged in user
        public virtual TKey AppUserId { get; set; } = default!;
        public virtual AppUser? AppUser { get; set; }
        
        // List of all gifts that are in this wishlist
        public virtual ICollection<Gift>? Gifts { get; set; }
        // List of all profiles that correspond to this wishlist
        public virtual ICollection<Profile>? Profiles { get; set; }
    }
}
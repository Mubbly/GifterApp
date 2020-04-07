using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

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

        public virtual TKey GiftId { get; set; } = default!;
        public virtual Gift? Gift { get; set; }  // TODO: Should be the other way around

        // List of all profiles that correspond to this wishlist
        public virtual ICollection<Profile>? Profiles { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Domain.Base;
using Domain.App.Identity;

namespace Domain.App
{
    public class Wishlist : Wishlist<Guid>
    {
        
    }

    /**
     * Wishlist is a list of Gifts that a user can create on their Profile
     * It is possible to see and interact with other users' Wishlists
     */
    public class Wishlist<TKey> : DomainEntityIdMetadataUser<AppUser>
        where TKey : struct, IEquatable<TKey>
    {
        [MaxLength(2048)] [MinLength(3)] public virtual string? Comment { get; set; }

        // AppUser

        // List of all gifts that are in this wishlist
        [InverseProperty(nameof(Gift.Wishlist))]
        public virtual ICollection<Gift>? Gifts { get; set; }

        // List of all profiles that correspond to this wishlist
        [InverseProperty(nameof(Profile.Wishlist))]
        public virtual ICollection<Profile>? Profiles { get; set; }
    }
}
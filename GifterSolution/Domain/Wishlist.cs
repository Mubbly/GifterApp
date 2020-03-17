using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    /**
     * Wishlist is a list of Gifts that a user can create on their Profile
     * It is possible to see and interact with other users' Wishlists
     */
    public class Wishlist : DomainEntity
    {
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }

        public Guid GiftId { get; set; } = default!;
        public Gift? Gift { get; set; }

        // List of all profiles that correspond to this wishlist
        public ICollection<Profile>? Profiles { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.App.DTO.Identity;
using com.mubbly.gifterapp.Contracts.Domain;

namespace BLL.App.DTO
{
    public class WishlistBLL : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUserBLL AppUser { get; set; } = default!;
        
        // List of all gifts that are in this wishlist
        [InverseProperty(nameof(GiftBLL.Wishlist))]
        public ICollection<GiftBLL>? Gifts { get; set; }
        // List of all profiles that correspond to this wishlist
        [InverseProperty(nameof(ProfileBLL.Wishlist))]
        public ICollection<ProfileBLL>? Profiles { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Contracts.Domain;
using DAL.App.DTO.Identity;
using Domain.App;

namespace DAL.App.DTO
{
    public class WishlistDAL : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUserDAL AppUser { get; set; } = default!;
        
        // List of all gifts that are in this wishlist
        [InverseProperty(nameof(GiftDAL.Wishlist))]
        public ICollection<GiftDAL>? Gifts { get; set; }
        // List of all profiles that correspond to this wishlist
        [InverseProperty(nameof(ProfileDAL.Wishlist))]
        public ICollection<ProfileDAL>? Profiles { get; set; }
    }
}
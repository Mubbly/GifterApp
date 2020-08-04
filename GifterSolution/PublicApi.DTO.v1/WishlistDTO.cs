using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.App.DTO;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class WishlistDTO
    {
        public Guid Id { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUserDTO? AppUser { get; set; } = default!;

        // List of all gifts that are in this wishlist
        // [InverseProperty(nameof(GiftDTO.Wishlist))]
        public ICollection<GiftDTO>? Gifts { get; set; }
        
        public int ProfilesCount { get; set; }
    }
}
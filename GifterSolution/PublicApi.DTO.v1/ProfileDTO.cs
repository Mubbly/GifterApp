﻿using System;
using System.ComponentModel.DataAnnotations;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class ProfileDTO
    {
        public Guid Id { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? ProfilePicture { get; set; }

        [MaxLength(256)] [MinLength(1)] public string? Gender { get; set; }

        [MaxLength(512)] [MinLength(1)] public string? Bio { get; set; }

        public int? Age { get; set; }
        public bool IsPrivate { get; set; }

        public Guid AppUserId { get; set; }
        public AppUserDTO? AppUser { get; set; } = default!;
        
        public Guid WishlistId { get; set; }
        public WishlistDTO? Wishlist { get; set; } = default!;

        public int UserProfilesCount { get; set; }
    }
}
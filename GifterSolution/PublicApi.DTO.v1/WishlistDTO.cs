using System;
using System.ComponentModel.DataAnnotations;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class WishlistDTO
    {
        public Guid Id { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUserDTO? AppUser { get; set; } = default!;

        public int GiftsCount { get; set; }
        public int ProfilesCount { get; set; }
    }
}
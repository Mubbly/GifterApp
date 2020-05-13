using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Wishlist : IDomainEntityId
    {
        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }

        //public AppUserDTO AppUser { get; set; } = default!;
        public AppUser AppUser { get; set; } = default!;

        public int GiftsCount { get; set; }
        public int ProfilesCount { get; set; }
        public Guid Id { get; set; }
    }
}
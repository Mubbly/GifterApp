using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class WishlistCreateDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }
        
        public Guid AppUserId { get; set; }
        //public AppUserDTO AppUser { get; set; } = default!;
    }
}
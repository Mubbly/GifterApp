using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class WishlistDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }

        public Guid GiftId { get; set; }

        public int ProfilesCount { get; set; }
    }
}
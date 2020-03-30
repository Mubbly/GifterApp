using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class GiftDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(256)] [MinLength(1)] 
        public string Name { get; set; } = default!;
        [MaxLength(1024)] [MinLength(3)] 
        public string? Description { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? Image { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? Url { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? PartnerUrl { get; set; }
        public bool IsPartnered { get; set; }
        public bool IsPinned { get; set; }

        public Guid ActionTypeId { get; set; }
        public Guid AppUserId { get; set; }
        public Guid StatusId { get; set; }
        
        public int WishlistsCount { get; set; }
        public int ReservedGiftsCount { get; set; }
        public int ArchivedGiftsCount { get; set; }
    }
}
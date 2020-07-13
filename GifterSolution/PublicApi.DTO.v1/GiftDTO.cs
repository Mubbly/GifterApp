using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class GiftDTO
    {
        public Guid Id { get; set; }

        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        [MaxLength(1024)] [MinLength(3)] public string? Description { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Image { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Url { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? PartnerUrl { get; set; }

        public bool IsPartnered { get; set; }
        public bool IsPinned { get; set; }
        
        // TODO: Move to separate GiftResponseDTO
        public DateTime? ReservedFrom { get; set; } = null;
        public DateTime? ArchivedFrom { get; set; } = null;
        public bool? IsArchivalConfirmed { get; set; } = false;
        public Guid? UserGiverId { get; set; } = null;
        public Guid? UserReceiverId { get; set; } = null;
        public string? UserReceiverName { get; set; } = null;
        public string? UserGiverName { get; set; } = null;

        public Guid ActionTypeId { get; set; }
        public ActionTypeDTO? ActionTypeDTO { get; set; } = default!;
        
        // public Guid AppUserId { get; set; }
        // public AppUserDTO AppUser { get; set; } = default!;
        
        public Guid StatusId { get; set; }
        public StatusDTO? Status { get; set; } = default!;
        
        public Guid WishlistId { get; set; }
        // public WishlistDTO? Wishlist { get; set; } = default!;

        public int ReservedGiftsCount { get; set; }
        public int ArchivedGiftsCount { get; set; }
    }
}
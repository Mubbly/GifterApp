using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.App.DTO.Identity;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class GiftBLL : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        [MaxLength(1024)] [MinLength(3)] public string? Description { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Image { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Url { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? PartnerUrl { get; set; }

        public bool IsPartnered { get; set; }
        public bool IsPinned { get; set; }

        public Guid ActionTypeId { get; set; }
        public ActionTypeBLL ActionType { get; set; } = default!;
        public Guid AppUserId { get; set; }
        public AppUserBLL AppUser { get; set; } = default!;
        public Guid StatusId { get; set; }
        public StatusBLL Status { get; set; } = default!;
        public Guid WishlistId { get; set; }
        public WishlistBLL Wishlist { get; set; } = default!;

        // List of all gifts that have reserved status
        [InverseProperty(nameof(ReservedGiftBLL.Gift))]
        public ICollection<ReservedGiftBLL>? ReservedGifts { get; set; }

        // List of all gifts that have archived status
        [InverseProperty(nameof(ArchivedGiftBLL.Gift))]
        public ICollection<ArchivedGiftBLL>? ArchivedGifts { get; set; }
    }
}
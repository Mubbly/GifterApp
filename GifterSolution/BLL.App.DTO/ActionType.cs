using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class ActionType : IDomainEntityId
    {
        [MaxLength(64)] [MinLength(1)] public string ActionTypeValue { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public int GiftsCount { get; set; }
        public int ReservedGiftsCount { get; set; }
        public int ArchivedGiftsCount { get; set; }
        public int DonateesCount { get; set; }
        public Guid Id { get; set; }

        // public virtual ICollection<Gift>? Gifts { get; set; }
        // public virtual ICollection<ReservedGift>? ReservedGifts { get; set; }
        // public virtual ICollection<Domain.App.ArchivedGift>? ArchivedGifts { get; set; }
        // public virtual ICollection<Donatee>? Donatees { get; set; }
    }
}
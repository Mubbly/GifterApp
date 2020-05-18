using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Contracts.Domain;

namespace BLL.App.DTO
{
    public class StatusBLL : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(64)] [MinLength(1)] public string StatusValue { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        // List of all gifts that correspond to this status
        [InverseProperty(nameof(GiftBLL.Status))]
        public ICollection<GiftBLL>? Gifts { get; set; }

        // List of all the reserved gifts that correspond to this status
        [InverseProperty(nameof(ReservedGiftBLL.Status))]
        public ICollection<ReservedGiftBLL>? ReservedGifts { get; set; }

        // List of all the archived gifts that correspond to this status
        [InverseProperty(nameof(ArchivedGiftBLL.Status))]
        public ICollection<ArchivedGiftBLL>? ArchivedGifts { get; set; }

        // List of all the donatees that correspond to this status
        [InverseProperty(nameof(DonateeBLL.Status))]
        public ICollection<DonateeBLL>? Donatees { get; set; }
    }
}
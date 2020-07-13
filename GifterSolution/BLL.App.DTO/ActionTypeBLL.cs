using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Contracts.Domain;

namespace BLL.App.DTO
{
    public class ActionTypeBLL : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(64)] [MinLength(1)] public string ActionTypeValue { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        // List of all the gifts that correspond to this action
        [InverseProperty(nameof(GiftBLL.ActionType))]
        public ICollection<GiftBLL>? Gifts { get; set; } // = new List<Gift>(); TODO: Should lists be initialized?

        // List of all the reserved gifts that correspond to this action
        [InverseProperty(nameof(ReservedGiftFullBLL.ActionType))]
        public ICollection<ReservedGiftFullBLL>? ReservedGifts { get; set; }

        // List of all the archived gifts that correspond to this action
        [InverseProperty(nameof(ArchivedGiftFullBLL.ActionType))]
        public ICollection<ArchivedGiftFullBLL>? ArchivedGifts { get; set; }

        // List of all the donatees that correspond to this action
        [InverseProperty(nameof(DonateeBLL.ActionType))]
        public ICollection<DonateeBLL>? Donatees { get; set; }
    }
}
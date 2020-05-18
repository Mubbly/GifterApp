using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Contracts.Domain;

namespace DAL.App.DTO
{
    public class ActionTypeDAL : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(64)] [MinLength(1)] public virtual string ActionTypeValue { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public virtual string? Comment { get; set; }

        // List of all the gifts that correspond to this action
        [InverseProperty(nameof(GiftDAL.ActionType))]
        public ICollection<GiftDAL>? Gifts { get; set; } // = new List<Gift>(); TODO: Should lists be initialized?

        // List of all the reserved gifts that correspond to this action
        [InverseProperty(nameof(ReservedGiftDAL.ActionType))]
        public ICollection<ReservedGiftDAL>? ReservedGifts { get; set; }

        // List of all the archived gifts that correspond to this action
        [InverseProperty(nameof(ArchivedGiftDAL.ActionType))]
        public ICollection<ArchivedGiftDAL>? ArchivedGifts { get; set; }

        // List of all the donatees that correspond to this action
        [InverseProperty(nameof(DonateeDAL.ActionType))]
        public ICollection<DonateeDAL>? Donatees { get; set; }
    }
}
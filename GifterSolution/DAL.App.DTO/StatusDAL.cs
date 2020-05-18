using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Contracts.Domain;

namespace DAL.App.DTO
{
    public class StatusDAL : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(64)] [MinLength(1)] public string StatusValue { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }
        
        // List of all gifts that correspond to this status
        [InverseProperty(nameof(GiftDAL.Status))]
        public ICollection<GiftDAL>? Gifts { get; set; }

        // List of all the reserved gifts that correspond to this status
        [InverseProperty(nameof(ReservedGiftDAL.Status))]
        public ICollection<ReservedGiftDAL>? ReservedGifts { get; set; }

        // List of all the archived gifts that correspond to this status
        [InverseProperty(nameof(ArchivedGiftDAL.Status))]
        public ICollection<ArchivedGiftDAL>? ArchivedGifts { get; set; }

        // List of all the donatees that correspond to this status
        [InverseProperty(nameof(DonateeDAL.Status))]
        public ICollection<DonateeDAL>? Donatees { get; set; }
    }
}
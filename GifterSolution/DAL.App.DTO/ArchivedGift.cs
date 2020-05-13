using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class ArchivedGift : IDomainEntityId
    {
        public DateTime DateArchived { get; set; }

        // "Receiver" of the gift has confirmed they have got it
        public bool IsConfirmed { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid ActionTypeId { get; set; }
        public ActionType ActionType { get; set; } = default!;
        public Guid GiftId { get; set; }
        public Gift Gift { get; set; } = default!;
        public Guid StatusId { get; set; }
        public Status Status { get; set; } = default!;
        public Guid UserGiverId { get; set; }
        public AppUser UserGiver { get; set; } = default!;
        public Guid UserReceiverId { get; set; }
        public AppUser UserReceiver { get; set; } = default!;
        public Guid Id { get; set; }
    }
}
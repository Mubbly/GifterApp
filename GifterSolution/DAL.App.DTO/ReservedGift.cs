using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class ReservedGift : IDomainEntityId
    {
        public DateTime ReservedFrom { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        // If gift has been reserved for 30 days, send a reminder to the "Reserver" user
        public DateTime DateToSendReminder => ReservedFrom.AddDays(30);
        public bool ShouldSendReminder => Convert.ToDateTime(DateToSendReminder).Equals(DateTime.Now);

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
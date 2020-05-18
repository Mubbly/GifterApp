using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class ReservedGiftDAL : IDomainEntityId
    {
        public Guid Id { get; set; }

        public DateTime ReservedFrom { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        // If gift has been reserved for 30 days, send a reminder to the "Reserver" user
        public DateTime DateToSendReminder => ReservedFrom.AddDays(30);
        public bool ShouldSendReminder => Convert.ToDateTime(DateToSendReminder).Equals(DateTime.Now);

        public Guid ActionTypeId { get; set; }
        public ActionTypeDAL ActionType { get; set; } = default!;
        
        public Guid GiftId { get; set; }
        public GiftDAL Gift { get; set; } = default!;
        
        public Guid StatusId { get; set; }
        public StatusDAL Status { get; set; } = default!;
        
        [ForeignKey(nameof(UserGiver))]
        public Guid UserGiverId { get; set; }
        public AppUserDAL UserGiver { get; set; } = default!;
        
        [ForeignKey(nameof(UserReceiver))]
        public Guid UserReceiverId { get; set; }
        public AppUserDAL UserReceiver { get; set; } = default!;
    }
}
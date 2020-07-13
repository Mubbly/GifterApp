using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.App.DTO.Identity;
using com.mubbly.gifterapp.Contracts.Domain;

namespace BLL.App.DTO
{
    public class ReservedGiftBLL
    {
        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid GiftId { get; set; }
        
        public Guid UserReceiverId { get; set; }
    }
    
    public class ReservedGiftResponseBLL : ReservedGiftBLL
    {
        public DateTime ReservedFrom { get; set; }
    }
    
    public class ReservedGiftFullBLL : IDomainEntityId
    {
        public Guid Id { get; set; }

        public DateTime ReservedFrom { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        // If gift has been reserved for 30 days, send a reminder to the "Reserver" user
        public DateTime DateToSendReminder => ReservedFrom.AddDays(30);
        public bool ShouldSendReminder => Convert.ToDateTime(DateToSendReminder).Equals(DateTime.Now);

        public Guid ActionTypeId { get; set; }
        public ActionTypeBLL ActionType { get; set; } = default!;
        
        public Guid GiftId { get; set; }
        public GiftBLL Gift { get; set; } = default!;
        
        public Guid StatusId { get; set; }
        public StatusBLL Status { get; set; } = default!;

        [ForeignKey(nameof(UserGiver))]
        public Guid UserGiverId { get; set; }
        public AppUserBLL UserGiver { get; set; } = default!;
        
        [ForeignKey(nameof(UserReceiver))]
        public Guid UserReceiverId { get; set; }
        public AppUserBLL UserReceiver { get; set; } = default!;
    }
}
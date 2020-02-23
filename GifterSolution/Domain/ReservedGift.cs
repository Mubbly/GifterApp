using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    /**
     * Gift in a "Reserved" status
     * It should be possible to identify who asked for it and who and when reserved it
     * Has special actions that can be taken (more info in ActionType.cs)
     */
    public class ReservedGift : DomainEntityMetadata
    {
        public DateTime ReservedFrom { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }
        
        // If gift has been reserved for 30 days, send a reminder to the "Reserver" user
        public DateTime DateToSendReminder => ReservedFrom.AddDays(30);
        public bool ShouldSendReminder => Convert.ToDateTime(DateToSendReminder).Equals(DateTime.Now);

        [MaxLength(36)]
        public string GiftId { get; set; } = default!;
        public Gift? Gift { get; set; }
        
        [MaxLength(36)]
        public string ActionTypeId { get; set; } = default!;
        public ActionType? ActionType { get; set; }
        
        [MaxLength(36)]
        public string StatusId { get; set; } = default!;
        public Status? Status { get; set; }

        // TODO: Manual connections where two users!
        [ForeignKey(nameof(UserGiver))] [MaxLength(36)]
        public string UserGiverId { get; set; } = default!;
        public AppUser? UserGiver { get; set; }
        
        [ForeignKey(nameof(UserReceiver))] [MaxLength(36)]
        public string UserReceiverId { get; set; } = default!;
        public AppUser? UserReceiver { get; set; }
    }
}
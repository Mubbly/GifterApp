using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class ReservedGift : ReservedGift<Guid>
    {
        
    }
    
    /**
     * Gift in a "Reserved" status
     * It should be possible to identify who asked for it and who and when reserved it
     * Has special actions that can be taken (more info in ActionType.cs)
     */
    public class ReservedGift<TKey> : DomainEntityIdMetadataUser<AppUser>
        where TKey : struct, IEquatable<TKey>
    {
        public virtual DateTime ReservedFrom { get; set; }

        [MaxLength(2048)] [MinLength(3)] public virtual string? Comment { get; set; }

        // If gift has been reserved for 30 days, send a reminder to the "Reserver" user
        public virtual DateTime DateToSendReminder => ReservedFrom.AddDays(30);
        public virtual bool ShouldSendReminder => Convert.ToDateTime(DateToSendReminder).Equals(DateTime.Now);

        public virtual TKey GiftId { get; set; } = default!;
        public virtual Gift? Gift { get; set; }

        public virtual TKey ActionTypeId { get; set; } = default!;
        public virtual ActionType? ActionType { get; set; }

        public virtual TKey StatusId { get; set; } = default!;
        public virtual Status? Status { get; set; }

        [ForeignKey(nameof(UserGiver))] 
        public virtual TKey UserGiverId { get; set; } = default!;
        public virtual AppUser? UserGiver { get; set; }

        [ForeignKey(nameof(UserReceiver))] 
        public virtual TKey UserReceiverId { get; set; } = default!;
        public virtual AppUser? UserReceiver { get; set; }
    }
}
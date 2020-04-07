using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class ArchivedGift : ArchivedGift<Guid>, IDomainEntity
    {
        
    }
    /**
     * Gift in an "Archieved" status
     * It should be possible to identify who asked for it and who gifted it
     * Has special actions that can be taken (more info in ActionType.cs)
     */
    public class ArchivedGift<TKey> : DomainEntity<TKey>
        where TKey: struct, IEquatable<TKey>
    {
        public virtual DateTime DateArchived { get; set; }
        // "Receiver" of the gift has confirmed they have got it
        public virtual bool IsConfirmed { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public virtual string? Comment { get; set; }

        public virtual TKey ActionTypeId { get; set; } = default!;
        public virtual ActionType? ActionType { get; set; }
        
        public virtual TKey GiftId { get; set; } = default!;
        public virtual Gift? Gift { get; set; }
        
        public virtual TKey StatusId { get; set; } = default!;
        public virtual Status? Status { get; set; }

        // TODO: Manual connection where two users!
        [ForeignKey(nameof(UserGiver))]
        public virtual TKey UserGiverId { get; set; } = default!;
        public virtual AppUser? UserGiver { get; set; }
        
        [ForeignKey(nameof(UserReceiver))]
        public virtual TKey UserReceiverId { get; set; } = default!;
        public virtual AppUser? UserReceiver { get; set; }
    }
}
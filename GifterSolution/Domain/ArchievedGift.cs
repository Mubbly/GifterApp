using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    /**
     * Gift in an "Archieved" status
     * It should be possible to identify who asked for it and who gifted it
     * Has special actions that can be taken (more info in ActionType.cs)
     */
    public class ArchivedGift : DomainEntityMetadata
    {
        public DateTime DateArchived { get; set; }
        // "Receiver" of the gift has confirmed they have got it
        public bool IsConfirmed { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }

        [MaxLength(36)] 
        public string ActionTypeId { get; set; } = default!;
        public ActionType? ActionType { get; set; }
        
        [MaxLength(36)]
        public string GiftId { get; set; } = default!;
        public Gift? Gift { get; set; }
        
        [MaxLength(36)]
        public string StatusId { get; set; } = default!;
        public Status? Status { get; set; }

        // TODO: Manual connection where two users!
        [ForeignKey(nameof(UserGiver))] [MaxLength(36)]
        public string UserGiverId { get; set; } = default!;
        public AppUser? UserGiver { get; set; }
        
        [ForeignKey(nameof(UserReceiver))] [MaxLength(36)]
        public string UserReceiverId { get; set; } = default!;
        public AppUser? UserReceiver { get; set; }
    }
}
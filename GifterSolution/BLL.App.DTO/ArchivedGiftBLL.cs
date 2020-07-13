using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.App.DTO.Identity;
using com.mubbly.gifterapp.Contracts.Domain;

namespace BLL.App.DTO
{
    public class ArchivedGiftBLL
    {
        public bool IsConfirmed { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid GiftId { get; set; }
        
        public Guid UserReceiverId { get; set; }
    }
    
    public class ArchivedGiftResponseBLL : ArchivedGiftBLL
    {
        public DateTime DateArchived { get; set; }
    }
    
    public class ArchivedGiftFullBLL : IDomainEntityId
    {
        public Guid Id { get; set; }

        public DateTime DateArchived { get; set; }

        // "Receiver" of the gift has confirmed they have got it
        public bool IsConfirmed { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

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
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class ArchivedGiftDAL : IDomainEntityId
    {
        public Guid Id { get; set; }

        public DateTime DateArchived { get; set; }

        // "Receiver" of the gift has confirmed they have got it
        public bool IsConfirmed { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

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
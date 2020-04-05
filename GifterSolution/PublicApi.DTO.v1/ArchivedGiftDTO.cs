using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class ArchivedGiftDTO
    {
        public Guid Id { get; set; }
        
        public DateTime DateArchived { get; set; }
        // "Receiver" of the gift has confirmed they have got it
        public bool IsConfirmed { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }

        public Guid ActionTypeId { get; set; }
        public ActionTypeDTO ActionType { get; set; } = default!;
        public Guid GiftId { get; set; }
        public GiftDTO Gift { get; set; } = default!;
        public Guid StatusId { get; set; }
        public StatusDTO Status { get; set; } = default!;
        public Guid UserGiverId { get; set; }
        public AppUsersDTO UserGiver { get; set; } = default!;
        public Guid UserReceiverId { get; set; }
        public AppUsersDTO UserReceiver { get; set; } = default!;
    }
}
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
        public Guid GiftId { get; set; }
        public Guid StatusId { get; set; }
        public Guid UserGiverId { get; set; }
        public Guid UserReceiverId { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class ReservedGiftDTO
    {
        public Guid Id { get; set; }
        
        public DateTime ReservedFrom { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }
        // If gift has been reserved for 30 days, send a reminder to the "Reserver" user
        public DateTime DateToSendReminder => ReservedFrom.AddDays(30);
        public bool ShouldSendReminder => Convert.ToDateTime(DateToSendReminder).Equals(DateTime.Now);
        
        public Guid ActionTypeId { get; set; }
        public Guid GiftId { get; set; }
        public Guid StatusId { get; set; }
        public Guid UserGiverId { get; set; }
        public Guid UserReceiverId { get; set; }
    }
}
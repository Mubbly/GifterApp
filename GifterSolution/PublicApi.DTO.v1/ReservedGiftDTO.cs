using System;
using System.ComponentModel.DataAnnotations;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class ReservedGiftDTO
    {
        public Guid Id { get; set; }

        public DateTime ReservedFrom { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        // If gift has been reserved for 30 days, send a reminder to the "Reserver" user
        public DateTime DateToSendReminder => ReservedFrom.AddDays(30);
        public bool ShouldSendReminder => Convert.ToDateTime(DateToSendReminder).Equals(DateTime.Now);

        public Guid ActionTypeId { get; set; }
        public ActionTypeDTO ActionTypeDTO { get; set; } = default!;
        public Guid GiftId { get; set; }
        public GiftDTO Gift { get; set; } = default!;
        public Guid StatusId { get; set; }
        public StatusDTO Status { get; set; } = default!;
        public Guid UserGiverId { get; set; }
        public AppUserDTO UserGiver { get; set; } = default!;
        public Guid UserReceiverId { get; set; }
        public AppUserDTO UserReceiver { get; set; } = default!;
    }
}
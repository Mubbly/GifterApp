using System;
using System.ComponentModel.DataAnnotations;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class ArchivedGiftDTO
    {
        public bool IsConfirmed { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid GiftId { get; set; }
        
        public Guid UserReceiverId { get; set; }
    }
    
    public class ArchivedGiftResponseDTO : ArchivedGiftDTO
    {
        public DateTime DateArchived { get; set; }
    }
    
    public class ArchivedGiftFullDTO
    {
        public Guid Id { get; set; }

        public DateTime DateArchived { get; set; }

        // "Receiver" of the gift has confirmed they have got it
        public bool IsConfirmed { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        // TODO : remove unnecessary stuff from public dtos
        
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
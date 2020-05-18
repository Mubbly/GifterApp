using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class DonateeDTO
    {
        public Guid Id { get; set; }

        [MaxLength(256)] [MinLength(1)] public string FirstName { get; set; } = default!;

        [MaxLength(256)] [MinLength(1)] public string? LastName { get; set; }

        [MaxLength(256)] [MinLength(1)] public string? Gender { get; set; }

        public int? Age { get; set; }

        [MaxLength(4096)] [MinLength(3)] public string? Bio { get; set; }

        [MaxLength(256)] [MinLength(1)] public string GiftName { get; set; } = default!;

        [MaxLength(1024)] [MinLength(3)] public string? GiftDescription { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? GiftImage { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? GiftUrl { get; set; }

        public bool IsActive { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public DateTime? GiftReservedFrom { get; set; }

        public string? FullName => FirstName + " " + LastName;

        public Guid ActionTypeId { get; set; }
        public ActionTypeDTO? ActionTypeDTO { get; set; } = default!;
        
        public Guid StatusId { get; set; }
        public StatusDTO? Status { get; set; } = default!;

        public int CampaignDonateesCount { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class DonateeDAL : IDomainEntityId
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
        public ActionTypeDAL? ActionType { get; set; } = default!;
        
        public Guid StatusId { get; set; }
        public StatusDAL? Status { get; set; } = default!;

        [InverseProperty(nameof(CampaignDonateeDAL.Donatee))]
        public ICollection<CampaignDonateeDAL>? DonateeCampaigns { get; set; }
    }
}
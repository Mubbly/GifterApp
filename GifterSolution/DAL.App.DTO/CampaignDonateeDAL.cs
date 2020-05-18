using System;
using System.ComponentModel.DataAnnotations;
using com.mubbly.gifterapp.Contracts.Domain;

namespace DAL.App.DTO
{
    public class CampaignDonateeDAL : IDomainEntityId
    {
        public Guid Id { get; set; }

        public bool IsActive { get; set; } = default!;

        [MaxLength(1024)] [MinLength(3)] public string? Comment { get; set; }

        public Guid CampaignId { get; set; }
        public CampaignDAL Campaign { get; set; } = default!;

        public Guid DonateeId { get; set; }
        public DonateeDAL Donatee { get; set; } = default!;
    }
}
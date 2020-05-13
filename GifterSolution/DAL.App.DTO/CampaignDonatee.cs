using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class CampaignDonatee : IDomainEntityId
    {
        public bool IsActive { get; set; } = default!;

        [MaxLength(1024)] [MinLength(3)] public string? Comment { get; set; }

        public Guid CampaignId { get; set; }
        public Campaign Campaign { get; set; } = default!;

        public Guid DonateeId { get; set; }
        public Donatee Donatee { get; set; } = default!;
        public Guid Id { get; set; }
    }
}
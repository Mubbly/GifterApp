using System;
using System.ComponentModel.DataAnnotations;
using com.mubbly.gifterapp.Contracts.Domain;

namespace BLL.App.DTO
{
    public class CampaignDonateeBLL : IDomainEntityId
    {
        public Guid Id { get; set; }

        public bool IsActive { get; set; } = default!;

        [MaxLength(1024)] [MinLength(3)] public string? Comment { get; set; }

        public Guid CampaignId { get; set; }
        public CampaignBLL Campaign { get; set; } = default!;

        public Guid DonateeId { get; set; }
        public DonateeBLL Donatee { get; set; } = default!;
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class CampaignDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(512)] [MinLength(1)] 
        public string Name { get; set; } = default!;
        [MaxLength(4096)] [MinLength(3)] 
        public string? Description { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? AdImage { get; set; }
        [MaxLength(512)] [MinLength(1)] 
        public string? Institution { get; set; }
        public DateTime ActiveFromDate { get; set; }
        public DateTime ActiveToDate { get; set; }
        public bool IsActive { get; set; }
        
        public int UserCampaignsCount { get; set; }
        public int CampaignDonateesCount { get; set; }
    }
}
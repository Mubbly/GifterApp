using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class CampaignDonateeDTO
    {
        public Guid Id { get; set; }
        
        public bool IsActive { get; set; } = default!;
        [MaxLength(1024)] [MinLength(3)] 
        public string? Comment { get; set; }
        
        public Guid CampaignId { get; set; }
        public Guid DonateeId { get; set; }
    }
}
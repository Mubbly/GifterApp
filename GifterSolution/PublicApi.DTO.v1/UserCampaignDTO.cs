using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class UserCampaignDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public Guid CampaignId { get; set; }
    }
}
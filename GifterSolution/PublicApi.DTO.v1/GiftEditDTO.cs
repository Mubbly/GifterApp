using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class GiftEditDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(256)] [MinLength(1)] 
        public string Name { get; set; } = default!;
        [MaxLength(1024)] [MinLength(3)] 
        public string? Description { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? Image { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? Url { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? PartnerUrl { get; set; }
        public bool IsPartnered { get; set; }
        public bool IsPinned { get; set; }

        public Guid ActionTypeId { get; set; }
        //public ActionTypeDTO ActionType { get; set; } = default!;
        public Guid AppUserId { get; set; }
        //public AppUsersDTO AppUser { get; set; } = default!;
        public Guid StatusId { get; set; }
        //public StatusDTO Status { get; set; } = default!;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    /**
     * Donatee is basically 2in1 UserProfile + Gift
     * These are temporary "person with a wish" tokens made by campaign manager user
     * so that participants wouldn't have to actually register one by one
     */
    public class Donatee : DomainEntity
    {
        [MaxLength(256)] [MinLength(1)] 
        public string FirstName { get; set; } = default!;
        [MaxLength(256)] [MinLength(1)] 
        public string? LastName { get; set; }
        [MaxLength(256)] [MinLength(1)] 
        public string? Gender { get; set; }
        public int? Age { get; set; }
        [MaxLength(4096)] [MinLength(3)] 
        public string? Bio { get; set; }
        [MaxLength(256)] [MinLength(1)] 
        public string GiftName { get; set; } = default!;
        [MaxLength(1024)] [MinLength(3)] 
        public string? GiftDescription { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? GiftImage { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? GiftUrl { get; set; }
        public DateTime? GiftReservedFrom { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public bool IsActive { get; set; }
        
        public string? FullName => FirstName + " " + LastName;

        public Guid ActionTypeId { get; set; } = default!;
        public ActionType? ActionType { get; set; }

        public Guid StatusId { get; set; } = default!;
        public Status? Status { get; set; }
        
        // List of mapped campaigns and donatees
        public ICollection<CampaignDonatee>? CampaignDonatees { get; set; }
    }
}
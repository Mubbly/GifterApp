using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class InvitedUserEditDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(128)] [MinLength(3)] 
        public string Email { get; set; } = default!;
        [MaxLength(32)] [MinLength(5)] 
        public string? PhoneNumber { get; set; }
        [MaxLength(1024)] [MinLength(3)] 
        public string? Message { get; set; }
    }
}
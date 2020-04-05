using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class InvitedUserDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(128)] [MinLength(3)] 
        public string Email { get; set; } = default!;
        [MaxLength(32)] [MinLength(5)] 
        public string? PhoneNumber { get; set; }
        [MaxLength(1024)] [MinLength(3)] 
        public string? Message { get; set; }
        public DateTime DateInvited { get; set; }
        public bool HasJoined { get; set; }

        public Guid InvitorUserId { get; set; }
        public AppUsersDTO InvitorUser { get; set; } = default!;
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    /**
     * Invited user is someone who has been invited to register
     * by an existing user via invitation link
     */
    public class InvitedUser : DomainEntityMetadata
    {
        [MaxLength(128)] [MinLength(3)] 
        public string Email { get; set; } = default!;
        [MaxLength(32)] [MinLength(5)] 
        public string? PhoneNumber { get; set; }
        [MaxLength(1024)] [MinLength(3)] 
        public string? Message { get; set; }
        public DateTime DateInvited { get; set; }
        public bool HasJoined { get; set; }

        [ForeignKey(nameof(InvitorUser))] [MaxLength(36)]
        public string InvitorUserId { get; set; } = default!;
        public AppUser? InvitorUser { get; set; }
    }
}
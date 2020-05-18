using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.App.DTO.Identity;
using com.mubbly.gifterapp.Contracts.Domain;

namespace BLL.App.DTO
{
    public class InvitedUserBLL : IDomainEntityId
    {
        [MaxLength(128)] [MinLength(3)] public string Email { get; set; } = default!;

        [MaxLength(32)] [MinLength(5)] public string? PhoneNumber { get; set; }

        [MaxLength(1024)] [MinLength(3)] public string? Message { get; set; }

        public DateTime DateInvited { get; set; }
        public bool HasJoined { get; set; }

        [ForeignKey(nameof(InvitorUser))]
        public Guid InvitorUserId { get; set; }
        public AppUserBLL InvitorUser { get; set; } = default!;
        public Guid Id { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class FriendshipDAL : IDomainEntityId
    {
        public Guid Id { get; set; }

        public bool IsConfirmed { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        // Requester
        [ForeignKey(nameof(AppUser1))]
        public Guid AppUser1Id { get; set; }
        public AppUserDAL AppUser1 { get; set; } = default!;

        // Addressee
        [ForeignKey(nameof(AppUser2))]
        public Guid AppUser2Id { get; set; }
        public AppUserDAL AppUser2 { get; set; } = default!;
    }
}
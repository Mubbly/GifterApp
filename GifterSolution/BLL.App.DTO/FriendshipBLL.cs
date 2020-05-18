using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.App.DTO.Identity;
using com.mubbly.gifterapp.Contracts.Domain;

namespace BLL.App.DTO
{
    public class FriendshipBLL : IDomainEntityId
    {
        public Guid Id { get; set; }

        public bool IsConfirmed { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        // Requester
        [ForeignKey(nameof(AppUser1))]
        public Guid AppUser1Id { get; set; }
        public AppUserBLL AppUser1 { get; set; } = default!;

        // Addressee
        [ForeignKey(nameof(AppUser2))]
        public Guid AppUser2Id { get; set; }
        public AppUserBLL AppUser2 { get; set; } = default!;
    }
}
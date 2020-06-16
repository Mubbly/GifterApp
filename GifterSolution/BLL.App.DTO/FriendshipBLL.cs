using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.App.DTO.Identity;
using com.mubbly.gifterapp.Contracts.Domain;

namespace BLL.App.DTO
{
    public class FriendshipResponseBLL : FriendshipBLL
    {
        public string Name { get; set; } = default!;
        public DateTime LastActive { get; set; } = default!;
    }
    
    public class FriendshipBLL : IDomainEntityId
    {
        public Guid Id { get; set; }

        public bool IsConfirmed { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        // Requester / current user
        [ForeignKey(nameof(AppUser1))]
        public Guid AppUser1Id { get; set; }
        public AppUserBLL AppUser1 { get; set; } = default!;

        // Addressee / friend user
        [ForeignKey(nameof(AppUser2))]
        public Guid AppUser2Id { get; set; }
        public AppUserBLL AppUser2 { get; set; } = default!;
    }
}
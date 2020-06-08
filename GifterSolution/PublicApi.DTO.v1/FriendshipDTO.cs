using System;
using System.ComponentModel.DataAnnotations;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class FriendshipDTO
    {
        public Guid Id { get; set; }

        public bool IsConfirmed { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        // // Requester
        // public Guid AppUser1Id { get; set; }
        //
        // public AppUserDTO? AppUser1 { get; set; } = default!;

        // Addressee / Friend user
        public Guid AppUser2Id { get; set; }
        public AppUserDTO? AppUser2 { get; set; } = default!;
    }
}
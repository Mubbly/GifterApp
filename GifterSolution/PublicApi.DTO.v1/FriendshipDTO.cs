using System;
using System.ComponentModel.DataAnnotations;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class FriendshipResponseDTO : FriendshipDTO
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime LastActive { get; set; } = default!;
    }
    
    public class FriendshipDTO
    {
        public Guid Id { get; set; }

        public bool IsConfirmed { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        // Addressee / Friend user
        public Guid AppUser2Id { get; set; }
        public AppUserDTO? AppUser2 { get; set; } = default!;
    }
}
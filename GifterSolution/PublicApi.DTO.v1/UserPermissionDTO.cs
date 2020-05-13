using System;
using System.ComponentModel.DataAnnotations;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class UserPermissionDTO
    {
        public Guid Id { get; set; }

        public DateTime From { get; set; } = default!;
        public DateTime To { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUserDTO AppUser { get; set; } = default!;
        public Guid PermissionId { get; set; }
        public PermissionDTO Permission { get; set; } = default!;
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class UserPermissionBLL : IDomainEntityId
    {
        public Guid Id { get; set; }

        public DateTime From { get; set; } = default!;
        public DateTime To { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUserBLL AppUser { get; set; } = default!;
        public Guid PermissionId { get; set; }
        public PermissionBLL Permission { get; set; } = default!;
    }
}
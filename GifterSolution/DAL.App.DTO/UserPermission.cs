using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class UserPermission : IDomainEntityId
    {
        public DateTime From { get; set; } = default!;
        public DateTime To { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; } = default!;
        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; } = default!;
        public Guid Id { get; set; }
    }
}
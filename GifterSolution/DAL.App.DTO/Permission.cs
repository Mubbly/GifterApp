using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class Permission : IDomainEntityId
    {
        [MaxLength(1024)] [MinLength(1)] public string PermissionValue { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public int UserPermissionsCount { get; set; }
        public Guid Id { get; set; }
    }
}
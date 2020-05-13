using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class PermissionDTO
    {
        public Guid Id { get; set; }

        [MaxLength(1024)] [MinLength(1)] public string PermissionValue { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public int UserPermissionsCount { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    /**
     * Temporary table - will actually preexist from Identity
     */
    public class UserPermission : DomainEntityMetadata
    {
        public DateTime From { get; set; } = default!;
        public DateTime To { get; set; } = default!;
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }

        [MaxLength(36)]
        public string AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        [MaxLength(36)]
        public string PermissionId { get; set; } = default!;
        public Permission? Permission { get; set; }

    }
}
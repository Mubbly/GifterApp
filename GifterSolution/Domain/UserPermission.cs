using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    /**
     * Temporary table - will actually preexist from Identity
     */
    public class UserPermission : DomainEntity
    {
        public DateTime From { get; set; } = default!;
        public DateTime To { get; set; } = default!;
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }

        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        public Guid PermissionId { get; set; } = default!;
        public Permission? Permission { get; set; }

    }
}
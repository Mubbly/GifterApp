using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class UserPermissionDAL : IDomainEntityId
    {
        public Guid Id { get; set; }

        public DateTime From { get; set; } = default!;
        public DateTime To { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUserDAL AppUser { get; set; } = default!;
        
        public Guid PermissionId { get; set; }
        public PermissionDAL Permission { get; set; } = default!;
    }
}
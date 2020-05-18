using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Contracts.Domain;

namespace BLL.App.DTO
{
    public class PermissionBLL : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(1024)] [MinLength(1)] public string PermissionValue { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        // List of all users that correspond to this permission
        [InverseProperty(nameof(UserPermissionBLL.Permission))]
        public virtual ICollection<UserPermissionBLL>? UserPermissions { get; set; }
    }
}
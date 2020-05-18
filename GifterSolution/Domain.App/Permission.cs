using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Domain.Base;

namespace Domain.App
{
    public class Permission : Permission<Guid>
    {
    }

    /**
     * Permissions restrict or allow users to do certain things within the app
     */
    public class Permission<TKey> : DomainEntityIdMetadata
        where TKey : struct, IEquatable<TKey>
    {
        [MaxLength(1024)] [MinLength(1)] public virtual string PermissionValue { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public virtual string? Comment { get; set; }

        // List of all users that correspond to this permission
        [InverseProperty(nameof(UserPermission.Permission))]
        public virtual ICollection<UserPermission>? UserPermissions { get; set; }
    }
}
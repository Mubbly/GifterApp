using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

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
        public virtual ICollection<UserPermission>? UserPermissions { get; set; }
    }
}
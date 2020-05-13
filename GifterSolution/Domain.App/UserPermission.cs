using System;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class UserPermission : UserPermission<Guid>
    {
    }

    public class UserPermission<TKey> : DomainEntityIdMetadata
        where TKey : struct, IEquatable<TKey>
    {
        public virtual DateTime From { get; set; } = default!;
        public virtual DateTime To { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public virtual string? Comment { get; set; }

        public virtual TKey PermissionId { get; set; } = default!;
        public virtual Permission? Permission { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class NotificationType : NotificationType<Guid>
    {
    }

    public class NotificationType<TKey> : DomainEntityIdMetadata
        where TKey : struct, IEquatable<TKey>
    {
        [MaxLength(64)] [MinLength(1)] public virtual string NotificationTypeValue { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public virtual string? Comment { get; set; }

        // List of all notifications that correspond to this type
        public virtual ICollection<Notification>? Notifications { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Domain.Base;

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
        [InverseProperty(nameof(Notification.NotificationType))]
        public virtual ICollection<Notification>? Notifications { get; set; }
    }
}
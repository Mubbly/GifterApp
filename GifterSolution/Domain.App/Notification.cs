using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Notification : Notification<Guid>
    {
        
    }
    
    /**
     * Notification can be about:
     *     new private message;
     *     not having added anything to the Wishlist for a long time;
     *     new Campaign starting;
     *     someone you invited to register joining
     *     ...or other.
     */
    public class Notification<TKey> : DomainEntityIdMetadata
        where TKey : struct, IEquatable<TKey>
    {
        [MaxLength(1024)] [MinLength(1)] public virtual string NotificationValue { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public virtual string? Comment { get; set; }

        public virtual TKey NotificationTypeId { get; set; } = default!;
        public virtual NotificationType? NotificationType { get; set; }

        // List of all users that correspond to this notification
        [InverseProperty(nameof(UserNotification.Notification))]
        public virtual ICollection<UserNotification>? UserNotifications { get; set; }
    }
}
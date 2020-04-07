using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class Notification : Notification<Guid>, IDomainEntity
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
    public class Notification<TKey> : DomainEntity<TKey>
        where TKey: struct, IEquatable<TKey>
    {
        [MaxLength(1024)] [MinLength(1)] 
        public virtual string NotificationValue { get; set; } = default!;
        [MaxLength(2048)] [MinLength(3)] 
        public virtual string? Comment { get; set; }
        
        public virtual TKey NotificationTypeId { get; set; } = default!;
        public virtual NotificationType? NotificationType { get; set; }

        // List of all users that correspond to this notification
        public virtual ICollection<UserNotification>? UserNotifications { get; set; }
    }
}
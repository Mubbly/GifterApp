using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    /**
     * Notification can be about:
     *     new private message;
     *     not having added anything to the Wishlist for a long time;
     *     new Campaign starting;
     *     someone you invited to register joining
     *     ...or other.
     */
    public class Notification : DomainEntity
    {
        [MaxLength(1024)] [MinLength(1)] 
        public string NotificationValue { get; set; } = default!;
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }
        
        public Guid NotificationTypeId { get; set; } = default!;
        public NotificationType? NotificationType { get; set; }

        // List of all users that correspond to this notification
        public ICollection<UserNotification>? UserNotifications { get; set; }
    }
}
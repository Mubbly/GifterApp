using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class NotificationType : DomainEntity
    {
        [MaxLength(64)] [MinLength(1)] 
        public string NotificationTypeValue { get; set; } = default!;
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }

        // List of all notifications that correspond to this type
        public ICollection<Notification>? Notifications { get; set; }
    }
}
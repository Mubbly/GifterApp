using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;

namespace Domain
{
    public class NotificationType : NotificationType<Guid>, IDomainEntity
    {
        
    }
    
    public class NotificationType<TKey> : DomainEntity<TKey>
        where TKey: struct, IEquatable<TKey>
    {
        [MaxLength(64)] [MinLength(1)] 
        public virtual string NotificationTypeValue { get; set; } = default!;
        [MaxLength(2048)] [MinLength(3)] 
        public virtual string? Comment { get; set; }

        // List of all notifications that correspond to this type
        public virtual ICollection<Notification>? Notifications { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class UserNotification : UserNotification<Guid>
    {
    }

    public class UserNotification<TKey> : DomainEntityIdMetadataUser<AppUser>
        where TKey : struct, IEquatable<TKey>
    {
        public virtual DateTime LastNotified { get; set; }
        public virtual DateTime RenotifyAt { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsDisabled { get; set; }

        [MaxLength(2048)] [MinLength(3)] public virtual string? Comment { get; set; }

        // AppUser

        public virtual TKey NotificationId { get; set; } = default!;
        public virtual Notification? Notification { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class UserNotification : UserNotification<Guid>, IDomainEntity
    {
        
    }
    
    public class UserNotification<TKey> : DomainEntity<TKey>
        where TKey: struct, IEquatable<TKey>
    {
        public virtual DateTime LastNotified { get; set; }
        public virtual DateTime RenotifyAt { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsDisabled { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public virtual string? Comment { get; set; }

        public virtual TKey AppUserId { get; set; } = default!;
        public virtual AppUser? AppUser { get; set; }

        public virtual TKey NotificationId { get; set; } = default!;
        public virtual Notification? Notification { get; set; }
    }
}
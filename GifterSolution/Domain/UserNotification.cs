using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class UserNotification : DomainEntity
    {
        public DateTime LastNotified { get; set; }
        public DateTime RenotifyAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDisabled { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }

        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        public Guid NotificationId { get; set; } = default!;
        public Notification? Notification { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class UserNotification : DomainEntityMetadata
    {
        public DateTime LastNotified { get; set; }
        public DateTime RenotifyAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDisabled { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }

        [MaxLength(36)] 
        public string AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        [MaxLength(36)]
        public string NotificationId { get; set; } = default!;
        public Notification? Notification { get; set; }
    }
}
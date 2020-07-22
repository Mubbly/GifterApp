using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using com.mubbly.gifterapp.Contracts.Domain;

namespace BLL.App.DTO
{
    public class UserNotificationEditBLL : IDomainEntityId
    {
        public Guid Id { get; set; }

        // public DateTime LastNotified { get; set; }
        // public DateTime RenotifyAt { get; set; }
        public bool IsActive { get; set; }
        // public bool IsDisabled { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public Guid NotificationId { get; set; }
    }
    
    public class UserNotificationBLL : IDomainEntityId
    {
        public Guid Id { get; set; }

        public DateTime LastNotified { get; set; }
        public DateTime RenotifyAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDisabled { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUserBLL AppUser { get; set; } = default!;
        public Guid NotificationId { get; set; }
        public NotificationBLL Notification { get; set; } = default!;
    }
}
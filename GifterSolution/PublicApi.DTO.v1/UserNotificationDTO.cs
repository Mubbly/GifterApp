using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class UserNotificationDTO
    {
        public Guid Id { get; set; }
        
        public DateTime LastNotified { get; set; }
        public DateTime RenotifyAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDisabled { get; set; }
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUserDTO AppUser { get; set; } = default!;
        public Guid NotificationId { get; set; }
        public NotificationDTO Notification { get; set; } = default!;
    }
}
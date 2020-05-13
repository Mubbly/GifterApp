using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class UserNotification : IDomainEntityId
    {
        public DateTime LastNotified { get; set; }
        public DateTime RenotifyAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDisabled { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; } = default!;
        public Guid NotificationId { get; set; }
        public Notification Notification { get; set; } = default!;
        public Guid Id { get; set; }
    }
}
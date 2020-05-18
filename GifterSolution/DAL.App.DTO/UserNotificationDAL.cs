using System;
using System.ComponentModel.DataAnnotations;
using com.mubbly.gifterapp.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class UserNotificationDAL : IDomainEntityId
    {
        public Guid Id { get; set; }

        public DateTime LastNotified { get; set; }
        public DateTime RenotifyAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsDisabled { get; set; }

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid AppUserId { get; set; }
        public AppUserDAL AppUser { get; set; } = default!;
        public Guid NotificationId { get; set; }
        public NotificationDAL Notification { get; set; } = default!;
    }
}
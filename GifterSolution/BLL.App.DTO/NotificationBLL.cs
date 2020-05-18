using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Contracts.Domain;

namespace BLL.App.DTO
{
    public class NotificationBLL : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(1024)] [MinLength(1)] public string NotificationValue { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid NotificationTypeId { get; set; }
        public NotificationTypeBLL NotificationType { get; set; } = default!;

        // List of all users that correspond to this notification
        [InverseProperty(nameof(UserNotificationBLL.Notification))]
        public ICollection<UserNotificationBLL>? UserNotifications { get; set; }
    }
}
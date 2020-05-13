using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;

namespace DAL.App.DTO
{
    public class Notification : IDomainEntityId
    {
        [MaxLength(1024)] [MinLength(1)] public string NotificationValue { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public Guid NotificationTypeId { get; set; }
        public NotificationType NotificationType { get; set; } = default!;

        public int UserNotificationsCount { get; set; }
        public Guid Id { get; set; }
    }
}
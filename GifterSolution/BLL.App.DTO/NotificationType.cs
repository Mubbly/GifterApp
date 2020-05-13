using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class NotificationType : IDomainEntityId
    {
        [MaxLength(64)] [MinLength(1)] public string NotificationTypeValue { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        public int NotificationsCount { get; set; }
        public Guid Id { get; set; }
    }
}
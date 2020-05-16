using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class NotificationTypeBLL : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(64)] [MinLength(1)] public string NotificationTypeValue { get; set; } = default!;

        [MaxLength(2048)] [MinLength(3)] public string? Comment { get; set; }

        // List of all notifications that correspond to this type
        [InverseProperty(nameof(NotificationBLL.NotificationType))]
        public ICollection<NotificationBLL>? Notifications { get; set; }
    }
}
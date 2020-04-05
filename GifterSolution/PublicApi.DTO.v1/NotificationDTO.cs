using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class NotificationDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(1024)] [MinLength(1)] 
        public string NotificationValue { get; set; } = default!;
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }
        
        public Guid NotificationTypeId { get; set; }
        public NotificationTypeDTO NotificationType { get; set; } = default!;

        public int UserNotificationsCount { get; set; }
    }
}
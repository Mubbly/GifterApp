using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class NotificationTypeDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(64)] [MinLength(1)] 
        public string NotificationTypeValue { get; set; } = default!;
        [MaxLength(2048)] [MinLength(3)] 
        public string? Comment { get; set; }
        
        public int NotificationsCount { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class PrivateMessageDTO
    {
        public Guid Id { get; set; }
        
        [MaxLength(4096)] [MinLength(1)] 
        public string Message { get; set; } = default!;
        public DateTime SentAt { get; set; }
        public bool IsSeen { get; set; }

        public Guid UserSenderId { get; set; }
        public Guid UserReceiverId { get; set; }
    }
}
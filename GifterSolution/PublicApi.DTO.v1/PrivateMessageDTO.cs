using System;
using System.ComponentModel.DataAnnotations;
using PublicApi.DTO.v1.Identity;

namespace PublicApi.DTO.v1
{
    public class PrivateMessageDTO
    {
        public Guid Id { get; set; }

        [MaxLength(4096)] [MinLength(1)] public string Message { get; set; } = default!;

        public DateTime SentAt { get; set; }
        public bool IsSeen { get; set; }

        public Guid UserSenderId { get; set; }
        public AppUserDTO? UserSender { get; set; } = default!;
        
        public Guid UserReceiverId { get; set; }
        public AppUserDTO UserReceiver { get; set; } = default!;
    }
}
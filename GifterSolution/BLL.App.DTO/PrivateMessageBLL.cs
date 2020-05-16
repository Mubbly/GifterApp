using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.App.DTO.Identity;
using Contracts.Domain;

namespace BLL.App.DTO
{
    public class PrivateMessageBLL : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(4096)] [MinLength(1)] public string Message { get; set; } = default!;

        public DateTime SentAt { get; set; }
        public bool IsSeen { get; set; }

        [ForeignKey(nameof(UserSender))]
        public Guid UserSenderId { get; set; }
        public AppUserBLL UserSender { get; set; } = default!;
        
        [ForeignKey(nameof(UserReceiver))]
        public Guid UserReceiverId { get; set; }
        public AppUserBLL UserReceiver { get; set; } = default!;
    }
}
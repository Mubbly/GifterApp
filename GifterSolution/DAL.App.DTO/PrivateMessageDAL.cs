using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class PrivateMessageDAL : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(4096)] [MinLength(1)] public string Message { get; set; } = default!;

        public DateTime SentAt { get; set; }
        public bool IsSeen { get; set; }

        [ForeignKey(nameof(UserSender))]
        public Guid UserSenderId { get; set; }
        public AppUserDAL UserSender { get; set; } = default!;
        
        [ForeignKey(nameof(UserReceiver))]
        public Guid UserReceiverId { get; set; }
        public AppUserDAL UserReceiver { get; set; } = default!;
    }
}
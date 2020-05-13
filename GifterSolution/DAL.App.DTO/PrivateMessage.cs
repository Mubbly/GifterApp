﻿using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class PrivateMessage : IDomainEntityId
    {
        [MaxLength(4096)] [MinLength(1)] public string Message { get; set; } = default!;

        public DateTime SentAt { get; set; }
        public bool IsSeen { get; set; }

        public Guid UserSenderId { get; set; }
        public AppUser UserSender { get; set; } = default!;
        public Guid UserReceiverId { get; set; }
        public AppUser UserReceiver { get; set; } = default!;
        public Guid Id { get; set; }
    }
}
﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    /**
     * Private message is a message sent from one user to another that only they can see
     * For example to ask for them to specify about something in their Wishlist
     */
    public class PrivateMessage : DomainEntityMetadata
    {
        [MaxLength(4096)] [MinLength(1)] 
        public string Message { get; set; } = default!;
        public DateTime SentAt { get; set; }
        public bool IsSeen { get; set; }
        
        // TODO: Manual connection
        [ForeignKey(nameof(UserSender))] [MaxLength(36)]
        public string UserSenderId { get; set; } = default!;
        public AppUser? UserSender { get; set; }
        
        [ForeignKey(nameof(UserReceiver))] [MaxLength(36)]
        public string UserReceiverId { get; set; } = default!;
        public AppUser? UserReceiver { get; set; }
    }
}
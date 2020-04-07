using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.DAL.Base;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class PrivateMessage : PrivateMessage<Guid>, IDomainEntity
    {
        
    }
    
    /**
     * Private message is a message sent from one user to another that only they can see
     * For example to ask for them to specify about something in their Wishlist
     */
    public class PrivateMessage<TKey> : DomainEntity<TKey>
        where TKey: struct, IEquatable<TKey>
    {
        [MaxLength(4096)] [MinLength(1)] 
        public virtual string Message { get; set; } = default!;
        public virtual DateTime SentAt { get; set; }
        public virtual bool IsSeen { get; set; }
        
        // TODO: Manual connection
        [ForeignKey(nameof(UserSender))]
        public virtual TKey UserSenderId { get; set; } = default!;
        public virtual AppUser? UserSender { get; set; }
        
        [ForeignKey(nameof(UserReceiver))]
        public virtual TKey UserReceiverId { get; set; } = default!;
        public virtual AppUser? UserReceiver { get; set; }
    }
}
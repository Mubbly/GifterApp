using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class PrivateMessage : PrivateMessage<Guid>
    {
        
    }
    
    /**
     * Private message is a message sent from one user to another that only they can see
     * For example to ask for them to specify about something in their Wishlist
     */
    public class PrivateMessage<TKey> : DomainEntityIdMetadataUser<AppUser>
        where TKey : struct, IEquatable<TKey>
    {
        [MaxLength(4096)] [MinLength(1)] public virtual string Message { get; set; } = default!;

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
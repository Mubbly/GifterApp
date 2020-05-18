using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Domain.Base;
using Domain.App.Identity;

namespace Domain.App
{
    public class InvitedUser : InvitedUser<Guid>
    {
        
    }
    
    /**
     * Invited user is someone who has been invited to register
     * by an existing user via invitation link
     */
    public class InvitedUser<TKey> : DomainEntityIdMetadata
        where TKey : struct, IEquatable<TKey>
    {
        [MaxLength(128)] [MinLength(3)] public virtual string Email { get; set; } = default!;

        [MaxLength(32)] [MinLength(5)] public virtual string? PhoneNumber { get; set; }

        [MaxLength(1024)] [MinLength(3)] public virtual string? Message { get; set; }

        public virtual DateTime DateInvited { get; set; }
        public virtual bool HasJoined { get; set; }

        [ForeignKey(nameof(InvitorUser))] 
        public virtual TKey InvitorUserId { get; set; } = default!;
        public virtual AppUser? InvitorUser { get; set; }
    }
}
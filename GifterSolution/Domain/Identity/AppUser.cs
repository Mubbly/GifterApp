using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
    
{
    public class AppUser : IdentityUser
    {
        // Overriden fields
        [MaxLength(36)] 
        public override string Id { get; set; } = default!; // To fix the PK length

        // Custom fields
        [MaxLength(256)] [MinLength(1)] 
        public string? FirstName { get; set; }
        [MaxLength(256)] [MinLength(1)] 
        public string? LastName { get; set; }
        public bool? IsCampaignManager { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? LastActive { get; set; }
        public DateTime? DateJoined { get; set; }
        
        public string? FullName => FirstName + " " + LastName;

        // List of all permissions that correspond to this user
        public ICollection<UserPermission>? UserPermissions { get; set; }
        
        // List of mapped users and their profiles
        public ICollection<UserProfile>? UserProfiles { get; set; }
        
        // List of all notifications that correspond to this user
        public ICollection<UserNotification>? UserNotifications { get; set; }

        // List of mapped (campaign manager) users and campaigns
        public ICollection<UserCampaign>? UserCampaigns { get; set; }
        
        // List of all gifts that correspond to this user
        public ICollection<Gift>? Gifts { get; set; }
        
        // List of all reserved gifts that correspond to this user
        public ICollection<ReservedGift>? ReservedGiftsByUser { get; set; }
        public ICollection<ReservedGift>? ReservedGiftsForUser { get; set; }

        // List of all archived gifts that correspond to this user
        public ICollection<ArchivedGift>? ArchivedGiftsByUser { get; set; }
        public ICollection<ArchivedGift>? ArchivedGiftsForUser { get; set; }

        // List of all friends/other users that correspond to this user
        public ICollection<Friendship>? ConfirmedFriendships { get; set; }
        public ICollection<Friendship>? PendingFriendships { get; set; }

        // List of all messages (sent or received) that correspond to this user
        [InverseProperty(nameof(PrivateMessage.UserSender))]
        public ICollection<PrivateMessage>? SentPrivateMessages { get; set; }
        [InverseProperty(nameof(PrivateMessage.UserReceiver))]
        public ICollection<PrivateMessage>? ReceivedPrivateMessages { get; set; }

        // List of all invited people that correspond to this user
        [InverseProperty(nameof(InvitedUser.InvitorUser))]
        public ICollection<InvitedUser>? InvitedUsers { get; set; }
    }
}
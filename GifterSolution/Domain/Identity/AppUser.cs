using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Base.Identity;

namespace Domain.Identity
    
{
    [Table("AspNetUsers")]
    public class AppUser : DomainIdentityEntity
    {
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
        [InverseProperty(nameof(ReservedGift.UserGiver))]
        public ICollection<ReservedGift>? ReservedGiftsByUser { get; set; }
        [InverseProperty(nameof(ReservedGift.UserReceiver))]
        public ICollection<ReservedGift>? ReservedGiftsForUser { get; set; }

        // List of all archived gifts that correspond to this user
        [InverseProperty(nameof(ArchivedGift.UserGiver))]
        public ICollection<ArchivedGift>? ArchivedGiftsByUser { get; set; }
        [InverseProperty(nameof(ArchivedGift.UserReceiver))]
        public ICollection<ArchivedGift>? ArchivedGiftsForUser { get; set; }

        // List of all friends/other users that correspond to this user
        [InverseProperty(nameof(Friendship.AppUser1))]
        public ICollection<Friendship>? ConfirmedFriendships { get; set; }
        [InverseProperty(nameof(Friendship.AppUser2))]
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
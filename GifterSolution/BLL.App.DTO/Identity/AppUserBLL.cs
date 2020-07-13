using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Contracts.Domain;

namespace BLL.App.DTO.Identity
{
    public class AppUserBLL : IDomainEntityId
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;

        // Custom fields
        
        [MaxLength(256)]
        [MinLength(1)]
        [Required]
        public string FirstName { get; set; } = default!;

        [MaxLength(256)]
        [MinLength(1)]
        [Required]
        public string LastName { get; set; } = default!;

        public bool IsCampaignManager { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public DateTime DateJoined { get; set; } = DateTime.Now;

        public string FullName => FirstName + " " + LastName;

        // List of all permissions that correspond to this user
        [InverseProperty(nameof(UserPermissionBLL.AppUser))]
        public ICollection<UserPermissionBLL>? UserPermissions { get; set; }
        // List of mapped users and their profiles
        [InverseProperty(nameof(UserProfileBLL.AppUser))]
        public ICollection<UserProfileBLL>? UserProfiles { get; set; }
        // List of all notifications that correspond to this user
        [InverseProperty(nameof(UserNotificationBLL.AppUser))]
        public ICollection<UserNotificationBLL>? UserNotifications { get; set; }
        // List of mapped (campaign manager) users and campaigns
        [InverseProperty(nameof(UserCampaignBLL.AppUser))]
        public ICollection<UserCampaignBLL>? UserCampaigns { get; set; }

        // List of all reserved gifts that correspond to this user
        [InverseProperty(nameof(ReservedGiftFullBLL.UserGiver))]
        public ICollection<ReservedGiftFullBLL>? ReservedGiftsByUser { get; set; }
        [InverseProperty(nameof(ReservedGiftFullBLL.UserReceiver))]
        public ICollection<ReservedGiftFullBLL>? ReservedGiftsForUser { get; set; }

        // List of all archived gifts that correspond to this user
        [InverseProperty(nameof(ArchivedGiftFullBLL.UserGiver))]
        public ICollection<ArchivedGiftFullBLL>? ArchivedGiftsByUser { get; set; }
        [InverseProperty(nameof(ArchivedGiftFullBLL.UserReceiver))]
        public ICollection<ArchivedGiftFullBLL>? ArchivedGiftsForUser { get; set; }

        // List of all friends/other users that correspond to this user
        [InverseProperty(nameof(FriendshipBLL.AppUser1))]
        public ICollection<FriendshipBLL>? ConfirmedFriendships { get; set; }
        [InverseProperty(nameof(FriendshipBLL.AppUser2))]
        public ICollection<FriendshipBLL>? PendingFriendships { get; set; }

        // List of all messages (sent or received) that correspond to this user
        [InverseProperty(nameof(PrivateMessageBLL.UserSender))]
        public ICollection<PrivateMessageBLL>? SentPrivateMessages { get; set; }
        [InverseProperty(nameof(PrivateMessageBLL.UserReceiver))]
        public ICollection<PrivateMessageBLL>? ReceivedPrivateMessages { get; set; }

        // List of all invited people that correspond to this user
        [InverseProperty(nameof(InvitedUserBLL.InvitorUser))]
        public ICollection<InvitedUserBLL>? InvitedUsers { get; set; }
    }
}
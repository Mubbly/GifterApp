using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain;

namespace DAL.App.DTO.Identity
{
    public class AppUserDAL : IDomainEntityId
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
        [InverseProperty(nameof(UserPermissionDAL.AppUser))]
        public ICollection<UserPermissionDAL>? UserPermissions { get; set; }
        // List of mapped users and their profiles
        [InverseProperty(nameof(UserProfileDAL.AppUser))]
        public ICollection<UserProfileDAL>? UserProfiles { get; set; }
        // List of all notifications that correspond to this user
        [InverseProperty(nameof(UserNotificationDAL.AppUser))]
        public ICollection<UserNotificationDAL>? UserNotifications { get; set; }
        // List of mapped (campaign manager) users and campaigns
        [InverseProperty(nameof(UserCampaignDAL.AppUser))]
        public ICollection<UserCampaignDAL>? UserCampaigns { get; set; }

        // List of all reserved gifts that correspond to this user
        [InverseProperty(nameof(ReservedGiftDAL.UserGiver))]
        public ICollection<ReservedGiftDAL>? ReservedGiftsByUser { get; set; }
        [InverseProperty(nameof(ReservedGiftDAL.UserReceiver))]
        public ICollection<ReservedGiftDAL>? ReservedGiftsForUser { get; set; }

        // List of all archived gifts that correspond to this user
        [InverseProperty(nameof(ArchivedGiftDAL.UserGiver))]
        public ICollection<ArchivedGiftDAL>? ArchivedGiftsByUser { get; set; }
        [InverseProperty(nameof(ArchivedGiftDAL.UserReceiver))]
        public ICollection<ArchivedGiftDAL>? ArchivedGiftsForUser { get; set; }

        // List of all friends/other users that correspond to this user
        [InverseProperty(nameof(FriendshipDAL.AppUser1))]
        public ICollection<FriendshipDAL>? ConfirmedFriendships { get; set; }
        [InverseProperty(nameof(FriendshipDAL.AppUser2))]
        public ICollection<FriendshipDAL>? PendingFriendships { get; set; }

        // List of all messages (sent or received) that correspond to this user
        [InverseProperty(nameof(PrivateMessageDAL.UserSender))]
        public ICollection<PrivateMessageDAL>? SentPrivateMessages { get; set; }
        [InverseProperty(nameof(PrivateMessageDAL.UserReceiver))]
        public ICollection<PrivateMessageDAL>? ReceivedPrivateMessages { get; set; }

        // List of all invited people that correspond to this user
        [InverseProperty(nameof(InvitedUserDAL.InvitorUser))]
        public ICollection<InvitedUserDAL>? InvitedUsers { get; set; }
    }
}
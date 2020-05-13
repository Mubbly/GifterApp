using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;

namespace DAL.App.DTO.Identity
{
    public class AppUser : IDomainEntityId
    {
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

        public ICollection<Domain.App.UserPermission>? UserPermissions { get; set; }
        public ICollection<Domain.App.UserProfile>? UserProfiles { get; set; }
        public ICollection<Domain.App.UserNotification>? UserNotifications { get; set; }
        public ICollection<Domain.App.UserCampaign>? UserCampaigns { get; set; }
        public ICollection<Domain.App.Gift>? Gifts { get; set; }
        public ICollection<Domain.App.ReservedGift>? ReservedGiftsByUser { get; set; }
        public ICollection<Domain.App.ReservedGift>? ReservedGiftsForUser { get; set; }
        public ICollection<Domain.App.ArchivedGift>? ArchivedGiftsByUser { get; set; }
        public ICollection<Domain.App.ArchivedGift>? ArchivedGiftsForUser { get; set; }
        public ICollection<Domain.App.Friendship>? ConfirmedFriendships { get; set; }
        public ICollection<Domain.App.Friendship>? PendingFriendships { get; set; }
        public ICollection<Domain.App.PrivateMessage>? SentPrivateMessages { get; set; }
        public ICollection<Domain.App.PrivateMessage>? ReceivedPrivateMessages { get; set; }
        public ICollection<Domain.App.InvitedUser>? InvitedUsers { get; set; }
        public Guid Id { get; set; }
    }
}
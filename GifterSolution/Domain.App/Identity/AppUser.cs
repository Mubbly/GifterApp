﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.mubbly.gifterapp.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    //[Table("AspNetUsers")]
    public class AppUser : AppUser<Guid>, IDomainEntityId
    {
        
    }

    public class AppUser<TKey> : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        // Custom fields
        [MaxLength(256)] [MinLength(1)] [Required]
        public string FirstName { get; set; } = default!;
        [MaxLength(256)] [MinLength(1)] [Required]
        public string LastName { get; set; } = default!;

        public bool IsCampaignManager { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public DateTime DateJoined { get; set; } = DateTime.Now;

        public string FullName => FirstName + " " + LastName;
        
        // // List of all gifts that correspond to this user
        // public ICollection<Gift>? Gifts { get; set; }
        
        // List of all permissions that correspond to this user
        [InverseProperty(nameof(UserPermission.AppUser))]
        public ICollection<UserPermission>? UserPermissions { get; set; }
        // List of mapped users and their profiles
        [InverseProperty(nameof(UserProfile.AppUser))]
        public ICollection<UserProfile>? UserProfiles { get; set; }
        // List of all notifications that correspond to this user
        [InverseProperty(nameof(UserNotification.AppUser))]
        public ICollection<UserNotification>? UserNotifications { get; set; }
        // List of mapped (campaign manager) users and campaigns
        [InverseProperty(nameof(UserCampaign.AppUser))]
        public ICollection<UserCampaign>? UserCampaigns { get; set; }

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
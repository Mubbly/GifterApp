using System;
using System.ComponentModel.DataAnnotations;
using com.mubbly.gifterapp.Contracts.Domain;

namespace PublicApi.DTO.v1.Identity
{
    public class AppUserDTO : IDomainEntityId
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        
        // Custom fields
        
        [MaxLength(256)] [MinLength(1)] [Required]
        public string FirstName { get; set; } = default!;

        [MaxLength(256)] [MinLength(1)] [Required]
        public string LastName { get; set; } = default!;
        
        public bool? IsCampaignManager { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? LastActive { get; set; }
        public DateTime? DateJoined { get; set; }
        
        public string? FullName => FirstName + " " + LastName;

        public int UserPermissionsCount { get; set; }
        public int UserProfilesCount { get; set; }
        public int UserNotificationsCount { get; set; }
        public int UserCampaignsCount { get; set; }
        public int GiftsCount { get; set; }
        public int ReservedGiftsByUserCount { get; set; }
        public int ReservedGiftsForUserCount { get; set; }
        public int ArchivedGiftsByUserCount { get; set; }
        public int ArchivedGiftsForUserCount { get; set; }
        public int ConfirmedFriendshipsCount { get; set; }
        public int PendingFriendshipsCount { get; set; }
        public int SentPrivateMessagesCount { get; set; }
        public int ReceivedPrivateMessagesCount { get; set; }
        public int InvitedUsersCount { get; set; }
    }
}
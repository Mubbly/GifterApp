using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class AppUserDTO
    {
        public Guid Id { get; set; }
        
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
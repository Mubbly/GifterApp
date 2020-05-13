using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        public IActionTypeService ActionTypes { get; }
        public IArchivedGiftService ArchivedGifts { get; }
        public ICampaignDonateeService CampaignDonatees { get; }
        public ICampaignService Campaigns { get; }
        public IDonateeService Donatees { get; }
        public IFriendshipService Friendships { get; }
        public IGiftService Gifts { get; }
        public IInvitedUserService InvitedUsers { get; }
        public INotificationService Notifications { get; }
        public INotificationTypeService NotificationTypes { get; }
        public IPermissionService Permissions { get; }
        public IPrivateMessageService PrivateMessages { get; }
        public IProfileService Profiles { get; }
        public IReservedGiftService ReservedGifts { get; }
        public IStatusService Statuses { get; }
        public IUserCampaignService UserCampaigns { get; }
        public IUserNotificationService UserNotifications { get; }
        public IUserPermissionService UserPermissions { get; }
        public IUserProfileService UserProfiles { get; }
        public IWishlistService Wishlists { get; }
    }
}
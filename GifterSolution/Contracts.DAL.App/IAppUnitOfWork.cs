using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
    {
        IActionTypeRepository ActionTypes { get; }

        IArchivedGiftRepository ArchivedGifts { get; }

        ICampaignRepository Campaigns { get; }

        ICampaignDonateeRepository CampaignDonatees { get; }

        IDonateeRepository Donatees { get; }

        IFriendshipRepository Friendships { get; }

        IGiftRepository Gifts { get; }

        IInvitedUserRepository InvitedUsers { get; }

        INotificationRepository Notifications { get; }

        INotificationTypeRepository NotificationTypes { get; }

        IPermissionRepository Permissions { get; }

        IPrivateMessageRepository PrivateMessages { get; }

        IProfileRepository Profiles { get; }

        IReservedGiftRepository ReservedGifts { get; }

        IStatusRepository Statuses { get; }

        IUserCampaignRepository UserCampaigns { get; }

        IUserNotificationRepository UserNotifications { get; }

        IUserPermissionRepository UserPermissions { get; }

        IUserProfileRepository UserProfiles { get; }

        IWishlistRepository Wishlists { get; }
    }
}
using BLL.App.Services;
using com.mubbly.gifterapp.BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork uow) : base(uow)
        {
        }
        
        public IActionTypeService ActionTypes =>
            GetService<IActionTypeService>(() => new ActionTypeService(UOW));

        public IArchivedGiftService ArchivedGifts =>
            GetService<IArchivedGiftService>(() => new ArchivedGiftService(UOW));

        public ICampaignService Campaigns =>
            GetService<ICampaignService>(() => new CampaignService(UOW));

        public ICampaignDonateeService CampaignDonatees =>
            GetService<ICampaignDonateeService>(() => new CampaignDonateeService(UOW));

        public IDonateeService Donatees =>
            GetService<IDonateeService>(() => new DonateeService(UOW));

        public IFriendshipService Friendships =>
            GetService<IFriendshipService>(() => new FriendshipService(UOW));

        public IGiftService Gifts =>
            GetService<IGiftService>(() => new GiftService(UOW));

        public IInvitedUserService InvitedUsers =>
            GetService<IInvitedUserService>(() => new InvitedUserService(UOW));

        public INotificationService Notifications =>
            GetService<INotificationService>(() => new NotificationService(UOW));

        public INotificationTypeService NotificationTypes =>
            GetService<INotificationTypeService>(() => new NotificationTypeService(UOW));

        public IPermissionService Permissions =>
            GetService<IPermissionService>(() => new PermissionService(UOW));

        public IPrivateMessageService PrivateMessages =>
            GetService<IPrivateMessageService>(() => new PrivateMessageService(UOW));

        public IProfileService Profiles =>
            GetService<IProfileService>(() => new ProfileService(UOW));

        public IReservedGiftService ReservedGifts =>
            GetService<IReservedGiftService>(() => new ReservedGiftService(UOW));

        public IStatusService Statuses =>
            GetService<IStatusService>(() => new StatusService(UOW));

        public IUserCampaignService UserCampaigns =>
            GetService<IUserCampaignService>(() => new UserCampaignService(UOW));

        public IUserNotificationService UserNotifications =>
            GetService<IUserNotificationService>(() => new UserNotificationService(UOW));

        public IUserPermissionService UserPermissions =>
            GetService<IUserPermissionService>(() => new UserPermissionService(UOW));

        public IUserProfileService UserProfiles =>
            GetService<IUserProfileService>(() => new UserProfileService(UOW));

        public IWishlistService Wishlists =>
            GetService<IWishlistService>(() => new WishlistService(UOW));
    }
}
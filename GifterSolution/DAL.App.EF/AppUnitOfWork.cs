using System;
using com.mubbly.gifterapp.DAL.Base.EF;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }

        public IActionTypeRepository ActionTypes =>
            GetRepository<IActionTypeRepository>(() => new ActionTypeRepository(UOWDbContext));

        public IArchivedGiftRepository ArchivedGifts =>
            GetRepository<IArchivedGiftRepository>(() => new ArchivedGiftRepository(UOWDbContext));

        public ICampaignRepository Campaigns =>
            GetRepository<ICampaignRepository>(() => new CampaignRepository(UOWDbContext));

        public ICampaignDonateeRepository CampaignDonatees =>
            GetRepository<ICampaignDonateeRepository>(() => new CampaignDonateeRepository(UOWDbContext));

        public IDonateeRepository Donatees =>
            GetRepository<IDonateeRepository>(() => new DonateeRepository(UOWDbContext));

        public IFriendshipRepository Friendships =>
            GetRepository<IFriendshipRepository>(() => new FriendshipRepository(UOWDbContext));

        public IGiftRepository Gifts =>
            GetRepository<IGiftRepository>(() => new GiftRepository(UOWDbContext));

        public IInvitedUserRepository InvitedUsers =>
            GetRepository<IInvitedUserRepository>(() => new InvitedUserRepository(UOWDbContext));

        public INotificationRepository Notifications =>
            GetRepository<INotificationRepository>(() => new NotificationRepository(UOWDbContext));

        public INotificationTypeRepository NotificationTypes =>
            GetRepository<INotificationTypeRepository>(() => new NotificationTypeRepository(UOWDbContext));

        public IPermissionRepository Permissions =>
            GetRepository<IPermissionRepository>(() => new PermissionRepository(UOWDbContext));

        public IPrivateMessageRepository PrivateMessages =>
            GetRepository<IPrivateMessageRepository>(() => new PrivateMessageRepository(UOWDbContext));

        public IProfileRepository Profiles =>
            GetRepository<IProfileRepository>(() => new ProfileRepository(UOWDbContext));

        public IReservedGiftRepository ReservedGifts =>
            GetRepository<IReservedGiftRepository>(() => new ReservedGiftRepository(UOWDbContext));

        public IStatusRepository Statuses =>
            GetRepository<IStatusRepository>(() => new StatusRepository(UOWDbContext));

        public IUserCampaignRepository UserCampaigns =>
            GetRepository<IUserCampaignRepository>(() => new UserCampaignRepository(UOWDbContext));

        public IUserNotificationRepository UserNotifications =>
            GetRepository<IUserNotificationRepository>(() => new UserNotificationRepository(UOWDbContext));

        public IUserPermissionRepository UserPermissions =>
            GetRepository<IUserPermissionRepository>(() => new UserPermissionRepository(UOWDbContext));

        public IUserProfileRepository UserProfiles =>
            GetRepository<IUserProfileRepository>(() => new UserProfileRepository(UOWDbContext));

        public IWishlistRepository Wishlists =>
            GetRepository<IWishlistRepository>(() => new WishlistRepository(UOWDbContext));
    }
}
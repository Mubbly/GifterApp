using AutoMapper;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using com.mubbly.gifterapp.DAL.Base.Mappers;
using Domain.App;
using Domain.App.Identity;
using Profile = Domain.App.Profile;

namespace DAL.App.EF.Mappers
{
    public class DALMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public DALMapper() : base()
        { 
            // User
            MapperConfigurationExpression.CreateMap<AppUser, AppUserDAL>();
            MapperConfigurationExpression.CreateMap<AppUserDAL, AppUser>();
            // Admin
            MapperConfigurationExpression.CreateMap<Permission, PermissionDAL>();
            MapperConfigurationExpression.CreateMap<PermissionDAL, Permission>();
            MapperConfigurationExpression.CreateMap<UserPermission, UserPermissionDAL>();
            MapperConfigurationExpression.CreateMap<UserPermissionDAL, UserPermission>();
            MapperConfigurationExpression.CreateMap<Notification, NotificationDAL>();
            MapperConfigurationExpression.CreateMap<NotificationDAL, Notification>();
            MapperConfigurationExpression.CreateMap<NotificationType, NotificationTypeDAL>();
            MapperConfigurationExpression.CreateMap<NotificationTypeDAL, NotificationType>();
            MapperConfigurationExpression.CreateMap<UserNotification, UserNotificationDAL>();
            MapperConfigurationExpression.CreateMap<UserNotificationDAL, UserNotification>();
            MapperConfigurationExpression.CreateMap<ActionTypeDAL, ActionType>();
            MapperConfigurationExpression.CreateMap<ActionType, ActionTypeDAL>();
            MapperConfigurationExpression.CreateMap<StatusDAL, Status>();
            MapperConfigurationExpression.CreateMap<Status, StatusDAL>();
            // Campaigns
            MapperConfigurationExpression.CreateMap<Campaign, CampaignDAL>();
            MapperConfigurationExpression.CreateMap<CampaignDAL, Campaign>();
            MapperConfigurationExpression.CreateMap<Donatee, DonateeDAL>();
            MapperConfigurationExpression.CreateMap<DonateeDAL, Donatee>();
            MapperConfigurationExpression.CreateMap<CampaignDonateeDAL, CampaignDonatee>();
            MapperConfigurationExpression.CreateMap<CampaignDonatee, CampaignDonateeDAL>();
            MapperConfigurationExpression.CreateMap<UserCampaignDAL, UserCampaign>();
            MapperConfigurationExpression.CreateMap<UserCampaign, UserCampaignDAL>();
            // Profile + Gifts
            MapperConfigurationExpression.CreateMap<Profile, ProfileDAL>();
            MapperConfigurationExpression.CreateMap<ProfileDAL, Profile>();
            MapperConfigurationExpression.CreateMap<UserProfile, UserProfileDAL>();
            MapperConfigurationExpression.CreateMap<UserProfileDAL, UserProfile>();
            MapperConfigurationExpression.CreateMap<Wishlist, WishlistDAL>();
            MapperConfigurationExpression.CreateMap<WishlistDAL, Wishlist>();
            MapperConfigurationExpression.CreateMap<Gift, GiftDAL>();
            MapperConfigurationExpression.CreateMap<GiftDAL, Gift>();
            MapperConfigurationExpression.CreateMap<ReservedGift, ReservedGiftDAL>();
            MapperConfigurationExpression.CreateMap<ReservedGiftDAL, ReservedGift>();
            MapperConfigurationExpression.CreateMap<ArchivedGift, ArchivedGiftDAL>();
            MapperConfigurationExpression.CreateMap<ArchivedGiftDAL, ArchivedGift>();
            // Other user interactions
            MapperConfigurationExpression.CreateMap<InvitedUser, InvitedUserDAL>();
            MapperConfigurationExpression.CreateMap<InvitedUserDAL, InvitedUser>();
            MapperConfigurationExpression.CreateMap<Friendship, FriendshipDAL>();
            MapperConfigurationExpression.CreateMap<FriendshipDAL, Friendship>();
            MapperConfigurationExpression.CreateMap<PrivateMessage, PrivateMessageDAL>();
            MapperConfigurationExpression.CreateMap<PrivateMessageDAL, PrivateMessage>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}
using AutoMapper;
using BLL.App.DTO;
using BLL.App.DTO.Identity;
using com.mubbly.gifterapp.BLL.Base.Mappers;
using DAL.App.DTO;
using DAL.App.DTO.Identity;
using Domain.App;

namespace BLL.App.Mappers
{
    public class BLLMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
        public BLLMapper() : base()
        {
            // User
            MapperConfigurationExpression.CreateMap<AppUserBLL, AppUserDAL>();
            MapperConfigurationExpression.CreateMap<AppUserDAL, AppUserBLL>();
            // Admin
            MapperConfigurationExpression.CreateMap<PermissionBLL, PermissionDAL>();
            MapperConfigurationExpression.CreateMap<PermissionDAL, PermissionBLL>();
            MapperConfigurationExpression.CreateMap<UserPermissionBLL, UserPermissionDAL>();
            MapperConfigurationExpression.CreateMap<UserPermissionDAL, UserPermissionBLL>();
            MapperConfigurationExpression.CreateMap<NotificationBLL, NotificationDAL>();
            MapperConfigurationExpression.CreateMap<NotificationDAL, NotificationBLL>();
            MapperConfigurationExpression.CreateMap<NotificationTypeBLL, NotificationTypeDAL>();
            MapperConfigurationExpression.CreateMap<NotificationTypeDAL, NotificationTypeBLL>();
            MapperConfigurationExpression.CreateMap<UserNotificationBLL, UserNotificationDAL>();
            MapperConfigurationExpression.CreateMap<UserNotificationDAL, UserNotificationBLL>();
            MapperConfigurationExpression.CreateMap<ActionTypeDAL, ActionTypeBLL>();
            MapperConfigurationExpression.CreateMap<ActionTypeBLL, ActionTypeDAL>();
            MapperConfigurationExpression.CreateMap<StatusDAL, StatusBLL>();
            MapperConfigurationExpression.CreateMap<StatusBLL, StatusDAL>();
            // Campaigns
            MapperConfigurationExpression.CreateMap<CampaignBLL, CampaignDAL>();
            MapperConfigurationExpression.CreateMap<CampaignDAL, CampaignBLL>();
            MapperConfigurationExpression.CreateMap<DonateeBLL, DonateeDAL>();
            MapperConfigurationExpression.CreateMap<DonateeDAL, DonateeBLL>();
            MapperConfigurationExpression.CreateMap<CampaignDonateeDAL, CampaignDonateeBLL>();
            MapperConfigurationExpression.CreateMap<CampaignDonateeBLL, CampaignDonateeDAL>();
            MapperConfigurationExpression.CreateMap<UserCampaignDAL, UserCampaignBLL>();
            MapperConfigurationExpression.CreateMap<UserCampaignBLL, UserCampaignDAL>();
            // Profile + Gifts
            MapperConfigurationExpression.CreateMap<ProfileBLL, ProfileDAL>();
            MapperConfigurationExpression.CreateMap<ProfileDAL, ProfileBLL>();
            MapperConfigurationExpression.CreateMap<UserProfileBLL, UserProfileDAL>();
            MapperConfigurationExpression.CreateMap<UserProfileDAL, UserProfileBLL>();
            MapperConfigurationExpression.CreateMap<WishlistBLL, WishlistDAL>();
            MapperConfigurationExpression.CreateMap<WishlistDAL, WishlistBLL>();
            MapperConfigurationExpression.CreateMap<GiftBLL, GiftDAL>();
            MapperConfigurationExpression.CreateMap<GiftDAL, GiftBLL>();
            MapperConfigurationExpression.CreateMap<ReservedGiftBLL, ReservedGiftDAL>();
            MapperConfigurationExpression.CreateMap<ReservedGiftDAL, ReservedGiftBLL>();
            MapperConfigurationExpression.CreateMap<ArchivedGiftBLL, ArchivedGiftDAL>();
            MapperConfigurationExpression.CreateMap<ArchivedGiftDAL, ArchivedGiftBLL>();
            // Other user interactions
            MapperConfigurationExpression.CreateMap<InvitedUserBLL, InvitedUserDAL>();
            MapperConfigurationExpression.CreateMap<InvitedUserDAL, InvitedUserBLL>();
            MapperConfigurationExpression.CreateMap<FriendshipBLL, FriendshipDAL>();
            MapperConfigurationExpression.CreateMap<FriendshipDAL, FriendshipBLL>();
            MapperConfigurationExpression.CreateMap<PrivateMessageBLL, PrivateMessageDAL>();
            MapperConfigurationExpression.CreateMap<PrivateMessageDAL, PrivateMessageBLL>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}
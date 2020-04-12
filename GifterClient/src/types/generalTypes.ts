import { IActionType } from "domain/IActionType";
import { IAppUser } from "domain/IAppUser";
import { IArchivedGift } from "domain/IArchivedGift";
import { ICampaign } from "domain/ICampaign";
import { ICampaignDonatee } from "domain/ICampaignDonatee";
import { IDonatee } from "domain/IDonatee";
import { IFriendship } from "domain/IFriendship";
import { IGift } from "domain/IGift";
import { IInvitedUser } from "domain/IInvitedUser";
import { INotification } from "domain/INotification";
import { INotificationType } from "domain/INotificationType";
import { IPermission } from "domain/IPermission";
import { IPrivateMessage } from "domain/IPrivateMessage";
import { IProfile } from "domain/IProfile";
import { IReservedGift } from "domain/IReservedGift";
import { IStatus } from "domain/IStatus";
import { IUserCampaign } from "domain/IUserCampaign";
import { IUserNotification } from "domain/IUserNotification";
import { IUserPermission } from "domain/IUserPermission";
import { IUserProfile } from "domain/IUserProfile";
import { IWishlist } from "domain/IWishlist";
import { ActionTypeService } from "service/actionTypeService";
import { CampaignService } from "service/campaignService";
import { CampaignDonateeService } from "service/campaignDonateeService";
import { StatusService } from "service/statusService";
import { AppUserService } from "service/appUserService";
import { ArchivedGiftService } from "service/archivedGiftService";
import { DonateeService } from "service/donateeService";
import { FriendshipService } from "service/friendshipService";
import { GiftService } from "service/giftService";
import { InvitedUserService } from "service/invitedUserService";
import { NotificationService } from "service/notificationService";
import { NotificationTypeService } from "service/notificationTypeService";
import { PermissionService } from "service/permissionService";
import { PrivateMessageService } from "service/privateMessageService";
import { ProfileService } from "service/profileService";
import { ReservedGiftService } from "service/reservedGiftService";
import { UserCampaignService } from "service/userCampaignService";
import { UserNotificationService } from "service/userNotificationService";
import { UserPermissionService } from "service/userPermissionService";
import { UserProfileService } from "service/userProfileService";
import { WishlistService } from "service/wishlistService";

export type Optional<TValue> = TValue | null;
export type ErrorMessage = string;
export type Id = string;
/** Expected format: YYYY-MM-DD */
export type HTML5DateString = string;

export enum GifterEntities {
    ActionType = "ActionType",
    AppUser = "AppUser",
    ArchivedGift = "ArchivedGift",
    Campaign = "Campaign",
    CampaignDonatee = "CampaignDonatee",
    Donatee = "Donatee",
    Friendship = "Friendship",
    Gift = "Gift",
    InvitedUser = "InvitedUser",
    Notification = "Notification",
    NotificationType = "NotificationType",
    Permission = "Permission",
    PrivateMessage = "PrivateMessage",
    Profile = "Profile",
    ReservedGift = "ReservedGift",
    Status = "Status",
    UserCampaign = "UserCampaign",
    UserNotification = "UserNotification",
    UserPermission = "UserPermission",
    UserProfile = "UserProfile",
    Wishlist = "Wishlist"
}

export type GifterEntity =
    | GifterEntities.ActionType
    | GifterEntities.AppUser
    | GifterEntities.ArchivedGift
    | GifterEntities.Campaign
    | GifterEntities.CampaignDonatee
    | GifterEntities.Donatee
    | GifterEntities.Friendship
    | GifterEntities.Gift
    | GifterEntities.InvitedUser
    | GifterEntities.Notification
    | GifterEntities.NotificationType
    | GifterEntities.Permission
    | GifterEntities.PrivateMessage
    | GifterEntities.Profile
    | GifterEntities.ReservedGift
    | GifterEntities.Status
    | GifterEntities.UserCampaign
    | GifterEntities.UserNotification
    | GifterEntities.UserPermission
    | GifterEntities.UserProfile
    | GifterEntities.Wishlist;

export type GifterInterface =
    | IActionType
    | IAppUser
    | IArchivedGift
    | ICampaign
    | ICampaignDonatee
    | IDonatee
    | IFriendship
    | IGift
    | IInvitedUser
    | INotification
    | INotificationType
    | IPermission
    | IPrivateMessage
    | IProfile
    | IReservedGift
    | IStatus
    | IUserCampaign
    | IUserNotification
    | IUserPermission
    | IUserProfile
    | IWishlist;

export type GeneralInterface<GifterInterface> = { props: GifterInterface };

export type GifterService =
    | ActionTypeService
    | AppUserService
    | ArchivedGiftService
    | CampaignService
    | CampaignDonateeService
    | DonateeService
    | FriendshipService
    | GiftService
    | InvitedUserService
    | NotificationService
    | NotificationTypeService
    | PermissionService
    | PrivateMessageService
    | ProfileService
    | ReservedGiftService
    | StatusService
    | UserCampaignService
    | UserNotificationService
    | UserPermissionService
    | UserProfileService
    | WishlistService;

export enum ViewTypes {
    Index = "Index",
    Details = "Details",
    Edit = "Edit",
    Delete = "Delete",
    Create = "Create"
}

export enum RequestTypes {
    CREATE = "POST",
    READ = "GET",
    UPDATE = "PUT",
    DELETE = "DELETE"
}

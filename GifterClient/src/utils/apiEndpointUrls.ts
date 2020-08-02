import * as DevEnvironment from '../../config/environment.json';
import * as ProdEnvironment from '../../config/environment.production.json';
import * as Utils from 'utils/utilFunctions';

// BASE URL
export const API_BASE_URL = DevEnvironment.backendUrl; // Utils.getEnvironmentProperties().backendUrl;

// Account
export const ACCOUNT_LOGIN = 'account/login';
export const ACCOUNT_REGISTER = 'account/register';
// Data
export const ACTION_TYPES = 'admin/ActionTypes';
export const APP_USERS = 'AppUsers';
export const ARCHIVED_GIFTS = 'ArchivedGifts';
export const CAMPAIGN_DONATEES = 'CampaignDonatees'
export const CAMPAIGNS = 'Campaigns';
export const DONATEES = 'Donatees';
export const FRIENDSHIPS = 'Friendships';
export const GIFTS = 'Gifts';
export const INVITED_USERS = 'InvitedUsers';
export const INVITED_USERS_PERSONAL = 'InvitedUsers/personal';
export const NOTIFICATIONS = 'Notifications';
export const NOTIFICATION_TYPES = 'admin/NotificationTypes';
export const PERMISSIONS = 'Permissions';
export const PRIVATE_MESSAGES = 'PrivateMessages';
export const PROFILES = 'Profiles';
export const PROFILES_PERSONAL = 'profiles/personal';
export const RESERVED_GIFTS = 'ReservedGifts';
export const STATUSES = 'admin/Statuses';
export const USER_CAMPAIGNS = 'UserCampaigns';
export const USER_NOTIFICATIONS = 'UserNotifications';
export const USER_PERMISSIONS = 'UserPermissions';
export const USER_PROFILES = 'UserProfiles';
export const WISHLISTS = 'Wishlists';
// States
export const RESERVED = 'Reserved';
export const ARCHIVED = 'Archived';
export const PENDING = 'Pending';
export const REACTIVATED = 'Reactivated';
export const GIVEN = 'Given';
export const RECEIVED = 'Received';
export const ACTIVE = 'Active';
export const INACTIVE = 'Inactive';
// User specific
export const PERSONAL = 'Personal';
export const USER = 'User';

import { PLATFORM } from 'aurelia-framework';

// First route should always be home view
export default [
        { route: ['', 'home', 'home/index'], name: 'homeIndex', moduleId: PLATFORM.moduleName('views/home/index'), nav: false, title: 'Home' },
        
        { route: ['account/login'], name: 'accountLogin', moduleId: PLATFORM.moduleName('views/account/login'), nav: false, title: 'Login' },
        { route: ['account/register'], name: 'accountRegister', moduleId: PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register' },



        { route: ['admin', 'admin/index'], name: 'adminIndex', moduleId: PLATFORM.moduleName('views/admin/index'), nav: false, title: 'Admin' },

        { route: ['notifications', 'notifications/index'], name: 'notificationsIndex', moduleId: PLATFORM.moduleName('views/notifications/index'), nav: false, title: 'Notifications' },
        { route: ['notifications/details/:id'], name: 'notificationsDetails', moduleId: PLATFORM.moduleName('views/notifications/details'), nav: false, title: 'NotificationsDetails' },
        { route: ['notifications/edit/:id?'], name: 'notificationsEdit', moduleId: PLATFORM.moduleName('views/notifications/edit'), nav: false, title: 'NotificationsEdit' },
        { route: ['notifications/delete/:id?'], name: 'notificationsDelete', moduleId: PLATFORM.moduleName('views/notifications/delete'), nav: false, title: 'NotificationsDelete' },
        { route: ['notifications/create'], name: 'notificationsCreate', moduleId: PLATFORM.moduleName('views/notifications/create'), nav: false, title: 'NotificationsCreate' },

        { route: ['notificationtypes', 'notificationtypes/index'], name: 'notificationtypesIndex', moduleId: PLATFORM.moduleName('views/notificationtypes/index'), nav: false, title: 'NotificationTypes' },
        { route: ['notificationtypes/details/:id'], name: 'notificationtypesDetails', moduleId: PLATFORM.moduleName('views/notificationtypes/details'), nav: false, title: 'NotificationTypesDetails' },
        { route: ['notificationtypes/edit/:id?'], name: 'notificationtypesEdit', moduleId: PLATFORM.moduleName('views/notificationtypes/edit'), nav: false, title: 'NotificationTypesEdit' },
        { route: ['notificationtypes/delete/:id?'], name: 'notificationtypesDelete', moduleId: PLATFORM.moduleName('views/notificationtypes/delete'), nav: false, title: 'NotificationTypesDelete' },
        { route: ['notificationtypes/create'], name: 'notificationtypesCreate', moduleId: PLATFORM.moduleName('views/notificationtypes/create'), nav: false, title: 'NotificationTypesCreate' },
        
        { route: ['permissions', 'permissions/index'], name: 'permissionsIndex', moduleId: PLATFORM.moduleName('views/permissions/index'), nav: false, title: 'Permissions' },
        { route: ['permissions/details/:id'], name: 'permissionsDetails', moduleId: PLATFORM.moduleName('views/permissions/details'), nav: false, title: 'PermissionsDetails' },
        { route: ['permissions/edit/:id?'], name: 'permissionsEdit', moduleId: PLATFORM.moduleName('views/permissions/edit'), nav: false, title: 'PermissionsEdit' },
        { route: ['permissions/delete/:id?'], name: 'permissionsDelete', moduleId: PLATFORM.moduleName('views/permissions/delete'), nav: false, title: 'PermissionsDelete' },
        { route: ['permissions/create'], name: 'permissionsCreate', moduleId: PLATFORM.moduleName('views/permissions/create'), nav: false, title: 'PermissionsCreate' },

        { route: ['actiontypes', 'actiontypes/index'], name: 'actiontypesIndex', moduleId: PLATFORM.moduleName('views/actiontypes/index'), nav: false, title: 'ActionTypes' },
        { route: ['actiontypes/details/:id'], name: 'actiontypesDetails', moduleId: PLATFORM.moduleName('views/actiontypes/details'), nav: false, title: 'ActionTypesDetails' },
        { route: ['actiontypes/edit/:id?'], name: 'actiontypesEdit', moduleId: PLATFORM.moduleName('views/actiontypes/edit'), nav: false, title: 'ActionTypesEdit' },
        { route: ['actiontypes/delete/:id?'], name: 'actiontypesDelete', moduleId: PLATFORM.moduleName('views/actiontypes/delete'), nav: false, title: 'ActionTypesDelete' },
        { route: ['actiontypes/create'], name: 'actiontypesCreate', moduleId: PLATFORM.moduleName('views/actiontypes/create'), nav: false, title: 'ActionTypesCreate' },

        { route: ['statuses', 'statuses/index'], name: 'statusesIndex', moduleId: PLATFORM.moduleName('views/statuses/index'), nav: false, title: 'Statuses' },
        { route: ['statuses/details/:id'], name: 'statusesDetails', moduleId: PLATFORM.moduleName('views/statuses/details'), nav: false, title: 'StatusesDetails' },
        { route: ['statuses/edit/:id?'], name: 'statusesEdit', moduleId: PLATFORM.moduleName('views/statuses/edit'), nav: false, title: 'StatusesEdit' },
        { route: ['statuses/delete/:id?'], name: 'statusesDelete', moduleId: PLATFORM.moduleName('views/statuses/delete'), nav: false, title: 'StatusesDelete' },
        { route: ['statuses/create'], name: 'statusesCreate', moduleId: PLATFORM.moduleName('views/statuses/create'), nav: false, title: 'StatusesCreate' },




        { route: ['profiles', 'profiles/index'], name: 'profilesIndex', moduleId: PLATFORM.moduleName('views/profiles/index'), nav: false, title: 'Profiles' },
        { route: ['profiles/personal'], name: 'profilesPersonal', moduleId: PLATFORM.moduleName('views/profiles/personal'), nav: true, title: 'My profile' },
        { route: ['profiles/details/:id'], name: 'profilesDetails', moduleId: PLATFORM.moduleName('views/profiles/details'), nav: false, title: 'ProfilesDetails' },
        { route: ['profiles/edit/:id?'], name: 'profilesEdit', moduleId: PLATFORM.moduleName('views/profiles/edit'), nav: false, title: 'ProfilesEdit' },
        { route: ['profiles/delete/:id?'], name: 'profilesDelete', moduleId: PLATFORM.moduleName('views/profiles/delete'), nav: false, title: 'ProfilesDelete' },
        { route: ['profiles/create'], name: 'profilesCreate', moduleId: PLATFORM.moduleName('views/profiles/create'), nav: false, title: 'ProfilesCreate' },

        { route: ['friendships', 'friendships/index'], name: 'friendshipsIndex', moduleId: PLATFORM.moduleName('views/friendships/index'), nav: true, title: 'Friends' },
        { route: ['friendships/details/:id'], name: 'friendshipsDetails', moduleId: PLATFORM.moduleName('views/friendships/details'), nav: false, title: 'FriendshipsDetails' },
        { route: ['friendships/edit/:id?'], name: 'friendshipsEdit', moduleId: PLATFORM.moduleName('views/friendships/edit'), nav: false, title: 'FriendshipsEdit' },
        { route: ['friendships/delete/:id?'], name: 'friendshipsDelete', moduleId: PLATFORM.moduleName('views/friendships/delete'), nav: false, title: 'FriendshipsDelete' },
        { route: ['friendships/create'], name: 'friendshipsCreate', moduleId: PLATFORM.moduleName('views/friendships/create'), nav: false, title: 'FriendshipsCreate' },

        { route: ['invitedusers', 'invitedusers/index'], name: 'invitedusersIndex', moduleId: PLATFORM.moduleName('views/invitedusers/index'), nav: false, title: 'InvitedUsers' },
        { route: ['invitedusers/details/:id'], name: 'invitedusersDetails', moduleId: PLATFORM.moduleName('views/invitedusers/details'), nav: false, title: 'InvitedUsersDetails' },
        { route: ['invitedusers/edit/:id?'], name: 'invitedusersEdit', moduleId: PLATFORM.moduleName('views/invitedusers/edit'), nav: false, title: 'InvitedUsersEdit' },
        { route: ['invitedusers/delete/:id?'], name: 'invitedusersDelete', moduleId: PLATFORM.moduleName('views/invitedusers/delete'), nav: false, title: 'InvitedUsersDelete' },
        { route: ['invitedusers/create'], name: 'invitedusersCreate', moduleId: PLATFORM.moduleName('views/invitedusers/create'), nav: false, title: 'InvitedUsersCreate' },
        
        { route: ['privatemessages', 'privatemessages/index'], name: 'privatemessagesIndex', moduleId: PLATFORM.moduleName('views/privatemessages/index'), nav: false, title: 'PrivateMessages' },
        { route: ['privatemessages/details/:id'], name: 'privatemessagesDetails', moduleId: PLATFORM.moduleName('views/privatemessages/details'), nav: false, title: 'PrivateMessagesDetails' },
        { route: ['privatemessages/edit/:id?'], name: 'privatemessagesEdit', moduleId: PLATFORM.moduleName('views/privatemessages/edit'), nav: false, title: 'PrivateMessagesEdit' },
        { route: ['privatemessages/delete/:id?'], name: 'privatemessagesDelete', moduleId: PLATFORM.moduleName('views/privatemessages/delete'), nav: false, title: 'PrivateMessagesDelete' },
        { route: ['privatemessages/create'], name: 'privatemessagesCreate', moduleId: PLATFORM.moduleName('views/privatemessages/create'), nav: false, title: 'PrivateMessagesCreate' },

        { route: ['wishlists', 'wishlists/index'], name: 'wishlistsIndex', moduleId: PLATFORM.moduleName('views/wishlists/index'), nav: false, title: 'Wishlists' },
        { route: ['wishlists/personal'], name: 'wishlistsPersonal', moduleId: PLATFORM.moduleName('views/wishlists/personal'), nav: false, title: 'WishlistsPersonal' },
        { route: ['wishlists/details/:id'], name: 'wishlistsDetails', moduleId: PLATFORM.moduleName('views/wishlists/details'), nav: false, title: 'WishlistsDetails' },
        { route: ['wishlists/edit/:id?'], name: 'wishlistsEdit', moduleId: PLATFORM.moduleName('views/wishlists/edit'), nav: false, title: 'WishlistsEdit' },
        { route: ['wishlists/delete/:id?'], name: 'wishlistsDelete', moduleId: PLATFORM.moduleName('views/wishlists/delete'), nav: false, title: 'WishlistsDelete' },
        { route: ['wishlists/create'], name: 'wishlistsCreate', moduleId: PLATFORM.moduleName('views/wishlists/create'), nav: false, title: 'WishlistsCreate' },        

        { route: ['gifts', 'gifts/index'], name: 'giftsIndex', moduleId: PLATFORM.moduleName('views/gifts/index'), nav: false, title: 'Gifts' },
        { route: ['gifts/details/:id'], name: 'giftsDetails', moduleId: PLATFORM.moduleName('views/gifts/details'), nav: false, title: 'GiftsDetails' },
        { route: ['gifts/edit/:id?'], name: 'giftsEdit', moduleId: PLATFORM.moduleName('views/gifts/edit'), nav: false, title: 'GiftsEdit' },
        { route: ['gifts/delete/:id?'], name: 'giftsDelete', moduleId: PLATFORM.moduleName('views/gifts/delete'), nav: false, title: 'GiftsDelete' },
        { route: ['gifts/create'], name: 'giftsCreate', moduleId: PLATFORM.moduleName('views/gifts/create'), nav: false, title: 'GiftsCreate' },

        { route: ['reservedgifts', 'reservedgifts/index'], name: 'reservedgiftsIndex', moduleId: PLATFORM.moduleName('views/reservedgifts/index'), nav: true, title: 'ToGift' },
        { route: ['reservedgifts/details/:id'], name: 'reservedgiftsDetails', moduleId: PLATFORM.moduleName('views/reservedgifts/details'), nav: false, title: 'ReservedGiftsDetails' },
        { route: ['reservedgifts/edit/:id?'], name: 'reservedgiftsEdit', moduleId: PLATFORM.moduleName('views/reservedgifts/edit'), nav: false, title: 'ReservedGiftsEdit' },
        { route: ['reservedgifts/delete/:id?'], name: 'reservedgiftsDelete', moduleId: PLATFORM.moduleName('views/reservedgifts/delete'), nav: false, title: 'ReservedGiftsDelete' },
        { route: ['reservedgifts/create'], name: 'reservedgiftsCreate', moduleId: PLATFORM.moduleName('views/reservedgifts/create'), nav: false, title: 'ReservedGiftsCreate' },

        { route: ['archivedgifts', 'archivedgifts/index'], name: 'archivedgiftsIndex', moduleId: PLATFORM.moduleName('views/archivedgifts/index'), nav: true, title: 'Archive' },
        { route: ['archivedgifts/details/:id'], name: 'archivedgiftsDetails', moduleId: PLATFORM.moduleName('views/archivedgifts/details'), nav: false, title: 'ArchivedGiftsDetails' },
        { route: ['archivedgifts/edit/:id?'], name: 'archivedgiftsEdit', moduleId: PLATFORM.moduleName('views/archivedgifts/edit'), nav: false, title: 'ArchivedGiftsEdit' },
        { route: ['archivedgifts/delete/:id?'], name: 'archivedgiftsDelete', moduleId: PLATFORM.moduleName('views/archivedgifts/delete'), nav: false, title: 'ArchivedGiftsDelete' },
        { route: ['archivedgifts/create'], name: 'archivedgiftsCreate', moduleId: PLATFORM.moduleName('views/archivedgifts/create'), nav: false, title: 'ArchivedGiftsCreate' },

        { route: ['campaigns', 'campaigns/index'], name: 'campaignsIndex', moduleId: PLATFORM.moduleName('views/campaigns/index'), nav: true, title: 'Campaigns' },
        { route: ['campaigns/personal'], name: 'campaignsPersonal', moduleId: PLATFORM.moduleName('views/campaigns/personal'), nav: false, title: 'CampaignsPersonal' },
        { route: ['campaigns/details/:id'], name: 'campaignsDetails', moduleId: PLATFORM.moduleName('views/campaigns/details'), nav: false, title: 'CampaignsDetails' },
        { route: ['campaigns/edit/:id?'], name: 'campaignsEdit', moduleId: PLATFORM.moduleName('views/campaigns/edit'), nav: false, title: 'CampaignsEdit' },
        { route: ['campaigns/delete/:id?'], name: 'campaignsDelete', moduleId: PLATFORM.moduleName('views/campaigns/delete'), nav: false, title: 'CampaignsDelete' },
        { route: ['campaigns/create'], name: 'campaignsCreate', moduleId: PLATFORM.moduleName('views/campaigns/create'), nav: false, title: 'CampaignsCreate' },

        { route: ['donatees', 'donatees/index'], name: 'donateesIndex', moduleId: PLATFORM.moduleName('views/donatees/index'), nav: false, title: 'Donatees' },
        { route: ['donatees/details/:id'], name: 'donateesDetails', moduleId: PLATFORM.moduleName('views/donatees/details'), nav: false, title: 'DonateesDetails' },
        { route: ['donatees/edit/:id?'], name: 'donateesEdit', moduleId: PLATFORM.moduleName('views/donatees/edit'), nav: false, title: 'DonateesEdit' },
        { route: ['donatees/delete/:id?'], name: 'donateesDelete', moduleId: PLATFORM.moduleName('views/donatees/delete'), nav: false, title: 'DonateesDelete' },
        { route: ['donatees/create'], name: 'donateesCreate', moduleId: PLATFORM.moduleName('views/donatees/create'), nav: false, title: 'DonateesCreate' },



        { route: ['campaigndonatees', 'campaigndonatees/index'], name: 'campaignDonateesIndex', moduleId: PLATFORM.moduleName('views/campaigndonatees/index'), nav: false, title: 'CampaignDonatees' },
        { route: ['campaigndonatees/details/:id'], name: 'campaigndonateesDetails', moduleId: PLATFORM.moduleName('views/campaigndonatees/details'), nav: false, title: 'CampaignDonateesDetails' },
        { route: ['campaigndonatees/edit/:id?'], name: 'campaigndonateesEdit', moduleId: PLATFORM.moduleName('views/campaigndonatees/edit'), nav: false, title: 'CampaignDonateesEdit' },
        { route: ['campaigndonatees/delete/:id?'], name: 'campaigndonateesDelete', moduleId: PLATFORM.moduleName('views/campaigndonatees/delete'), nav: false, title: 'CampaignDonateesDelete' },
        { route: ['campaigndonatees/create'], name: 'campaigndonateesCreate', moduleId: PLATFORM.moduleName('views/campaigndonatees/create'), nav: false, title: 'CampaignDonateesCreate' },

        { route: ['usercampaigns', 'usercampaigns/index'], name: 'usercampaignsIndex', moduleId: PLATFORM.moduleName('views/usercampaigns/index'), nav: false, title: 'UserCampaigns' },
        { route: ['usercampaigns/details/:id'], name: 'usercampaignsDetails', moduleId: PLATFORM.moduleName('views/usercampaigns/details'), nav: false, title: 'UserCampaignsDetails' },
        { route: ['usercampaigns/edit/:id?'], name: 'usercampaignsEdit', moduleId: PLATFORM.moduleName('views/usercampaigns/edit'), nav: false, title: 'UserCampaignsEdit' },
        { route: ['usercampaigns/delete/:id?'], name: 'usercampaignsDelete', moduleId: PLATFORM.moduleName('views/usercampaigns/delete'), nav: false, title: 'UserCampaignsDelete' },
        { route: ['usercampaigns/create'], name: 'usercampaignsCreate', moduleId: PLATFORM.moduleName('views/usercampaigns/create'), nav: false, title: 'UserCampaignsCreate' },

        { route: ['usernotifications', 'usernotifications/index'], name: 'usernotificationsIndex', moduleId: PLATFORM.moduleName('views/usernotifications/index'), nav: false, title: 'UserNotifications' },
        { route: ['usernotifications/details/:id'], name: 'usernotificationsDetails', moduleId: PLATFORM.moduleName('views/usernotifications/details'), nav: false, title: 'UserNotificationsDetails' },
        { route: ['usernotifications/edit/:id?'], name: 'usernotificationsEdit', moduleId: PLATFORM.moduleName('views/usernotifications/edit'), nav: false, title: 'UserNotificationsEdit' },
        { route: ['usernotifications/delete/:id?'], name: 'usernotificationsDelete', moduleId: PLATFORM.moduleName('views/usernotifications/delete'), nav: false, title: 'UserNotificationsDelete' },
        { route: ['usernotifications/create'], name: 'usernotificationsCreate', moduleId: PLATFORM.moduleName('views/usernotifications/create'), nav: false, title: 'UserNotificationsCreate' },

        { route: ['userpermissions', 'userpermissions/index'], name: 'userpermissionsIndex', moduleId: PLATFORM.moduleName('views/userpermissions/index'), nav: false, title: 'UserPermissions' },
        { route: ['userpermissions/details/:id'], name: 'userpermissionsDetails', moduleId: PLATFORM.moduleName('views/userpermissions/details'), nav: false, title: 'UserPermissionsDetails' },
        { route: ['userpermissions/edit/:id?'], name: 'userpermissionsEdit', moduleId: PLATFORM.moduleName('views/userpermissions/edit'), nav: false, title: 'UserPermissionsEdit' },
        { route: ['userpermissions/delete/:id?'], name: 'userpermissionsDelete', moduleId: PLATFORM.moduleName('views/userpermissions/delete'), nav: false, title: 'UserPermissionsDelete' },
        { route: ['userpermissions/create'], name: 'userpermissionsCreate', moduleId: PLATFORM.moduleName('views/userpermissions/create'), nav: false, title: 'UserPermissionsCreate' },

        { route: ['userprofiles', 'userprofiles/index'], name: 'userprofilesIndex', moduleId: PLATFORM.moduleName('views/userprofiles/index'), nav: false, title: 'UserProfiles' },
        { route: ['userprofiles/details/:id'], name: 'userprofilesDetails', moduleId: PLATFORM.moduleName('views/userprofiles/details'), nav: false, title: 'UserProfilesDetails' },
        { route: ['userprofiles/edit/:id?'], name: 'userprofilesEdit', moduleId: PLATFORM.moduleName('views/userprofiles/edit'), nav: false, title: 'UserProfilesEdit' },
        { route: ['userprofiles/delete/:id?'], name: 'userprofilesDelete', moduleId: PLATFORM.moduleName('views/userprofiles/delete'), nav: false, title: 'UserProfilesDelete' },
        { route: ['userprofiles/create'], name: 'userprofilesCreate', moduleId: PLATFORM.moduleName('views/userprofiles/create'), nav: false, title: 'UserProfilesCreate' },
]

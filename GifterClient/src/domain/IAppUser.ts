import { IBaseEntity } from "./base/IBaseEntity";

export interface ICurrentUser extends IBaseEntity {
    firstName: string,
    lastName: string
}

export interface IAppUser extends IBaseEntity {
    email: string;
    userName: string;
    firstName: string;
    lastName: string;
    fullName: string;
    lastActive: string;
    isCampaignManager: boolean;
    isActive: boolean;
    dateJoined: string;

    // userPermissionsCount: number;
    // userProfilesCount: number;
    // userNotificationsCount: number;
    // userCampaignsCount: number;
    // giftsCount: number;
    // reservedGiftsByUserCount: number;
    // reservedGiftsForUserCount: number;
    // archivedGiftsByUserCount: number;
    // archivedGiftsForUserCount: number;
    // etc
}

export interface IAppUserEdit extends IBaseEntity {
}

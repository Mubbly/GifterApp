import { Optional } from "types/generalTypes";
import { IAppUser } from "./IAppUser";
import { INotification } from "./INotification";

export interface IUserNotification {
    id: string;
    lastNotified: string;
    renotifyAt: string;
    isActive: boolean;
    isDisabled: boolean;
    comment: Optional<string>;

    appUserId: string;
    appUser: IAppUser;

    notificationId: string;
    notification: INotification;
}

import { Optional } from "types/generalTypes";
import { INotificationType } from './INotificationType';
import { IBaseEntity, IBaseEditEntity } from './base/IBaseEntity';

export interface INotificationCreate {
    notificationValue: string;
    comment: Optional<string>;

    notificationTypeId: string;
}

export interface INotificationEdit extends IBaseEntity, INotificationCreate {
}

export interface IUserNotificationEdit extends IBaseEditEntity {
    isActive: boolean;
    comment: Optional<string>;
    notificationId: string;
    appUserId: string;
}

export interface INotification extends INotificationEdit {
    notificationType: INotificationType;
}

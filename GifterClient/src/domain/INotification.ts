import { Optional } from "types/generalTypes";
import { INotificationType } from './INotificationType';
import { IBaseEntity } from "./base/IBaseEntity";

export interface INotificationCreate {
    notificationValue: string;
    comment: Optional<string>;

    notificationTypeId: string;
}

export interface INotificationEdit extends IBaseEntity, INotificationCreate {
}

export interface INotification extends INotificationEdit {
    notificationType: INotificationType;
}

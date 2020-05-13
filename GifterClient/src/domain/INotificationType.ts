import { Optional } from "types/generalTypes";
import { IBaseEntity } from "./base/IBaseEntity";

export interface INotificationTypeCreate {
    notificationTypeValue: string;
    comment: Optional<string>;
}

export interface INotificationTypeEdit extends IBaseEntity, INotificationTypeCreate {
}

export interface INotificationType extends INotificationTypeEdit {
    notificationsCount: number;
}

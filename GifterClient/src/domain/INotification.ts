import { Optional } from "types/generalTypes";
import { INotificationType } from './INotificationType';

export interface INotification {
    id: string;
    notificationValue: string;
    comment: Optional<string>;

    notificationTypeId: string;
    notificationType: INotificationType;
}

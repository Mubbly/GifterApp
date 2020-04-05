import { Optional } from "types/generalTypes";

export interface INotificationType {
    id: string;
    notificationTypeValue: string;
    comment: Optional<string>;

    notificationsCount: number;
}

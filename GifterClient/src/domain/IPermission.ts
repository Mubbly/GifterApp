import { Optional } from "types/generalTypes";
import { INotificationType } from "./INotificationType";

export interface IPermission {
    id: string;
    permissionValue: string;
    comment: Optional<string>;

    userPermissionsCount: number;
}

import { Optional } from "types/generalTypes";
import { INotificationType } from "./INotificationType";
import { IBaseEntity } from "./base/IBaseEntity";

export interface IPermissionCreate {
    permissionValue: string;
    comment: Optional<string>;
}

export interface IPermissionEdit extends IBaseEntity, IPermissionCreate {
}

export interface IPermission extends IPermissionEdit {
    userPermissionsCount: number;
}

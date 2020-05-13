import { Optional } from "types/generalTypes";
import { IAppUser } from "./IAppUser";
import { IBaseEntity } from "./base/IBaseEntity";

export interface IInvitedUserCreate {
    email: string;
    phoneNumber: Optional<string>;
    message: Optional<string>;
    dateInvited: string;
}

export interface IInvitedUserEdit extends IBaseEntity, IInvitedUserCreate {
}

export interface IInvitedUser extends IInvitedUserEdit {
    hasJoined: boolean;
    
    invitorUserId: string;
    invitorUser: IAppUser;
}

import { Optional } from "types/generalTypes";
import { IAppUser } from "./IAppUser";
import { IBaseEntity } from "./base/IBaseEntity";

export interface IFriendshipCreate {
    comment: Optional<string>;
    friendUserId: string;
}

export interface IFriendshipEdit extends IBaseEntity {
    isConfirmed: boolean;
    friendUserId: string;
}

export interface IFriendship extends IBaseEntity {
    comment: Optional<string>;
    isConfirmed: boolean;
    friendUserId: string;
}

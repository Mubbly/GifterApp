import { Optional } from "types/generalTypes";
import { IAppUser } from "./IAppUser";
import { IBaseEntity } from "./base/IBaseEntity";

export interface IFriendshipCreate {
    appUser2Id: string; // friend id
    comment: Optional<string>;
}

export interface IFriendshipEdit extends IBaseEntity {
    isConfirmed: boolean;
    appUser2Id: string; // friend id
}

export interface IFriendship extends IBaseEntity {
    comment: Optional<string>;
    isConfirmed: boolean;
    appUser2Id: string; // friend id
}

export interface IFriendshipResponse extends IFriendship {
    name: string;
    email: string;
    lastActive: string;
}

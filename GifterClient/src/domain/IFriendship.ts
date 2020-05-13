import { Optional } from "types/generalTypes";
import { IAppUser } from "./IAppUser";
import { IBaseEntity } from "./base/IBaseEntity";

export interface IFriendshipCreate {
    isConfirmed: boolean;
    comment: Optional<string>;

    appUser1Id: string;
    appUser2Id: string;
}

export interface IFriendshipEdit extends IBaseEntity {
    appUser1Id: string;
    appUser2Id: string;
}

export interface IFriendship extends IBaseEntity, IFriendshipCreate {
    appUser1: IAppUser;
    appUser2: IAppUser;
}

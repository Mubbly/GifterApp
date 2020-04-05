import { Optional } from "types/generalTypes";
import { IAppUser } from "./IAppUser";

export interface IFriendship {
    id: string;
    isConfirmed: boolean;
    comment: Optional<string>;

    appUser1Id: string;
    appUser1: IAppUser;

    appUser2Id: string;
    appUser2: IAppUser;
}

import { Optional } from "types/generalTypes";
import { IAppUser } from "./IAppUser";

export interface IInvitedUser {
    id: string;
    email: string;
    phoneNumber: Optional<string>;
    message: Optional<string>;
    dateInvited: string;
    hasJoined: boolean;

    invitorUserId: string;
    invitorUser: IAppUser;
}

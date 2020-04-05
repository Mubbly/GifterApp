import { Optional } from "types/generalTypes";
import { IAppUser } from "./IAppUser";
import { IProfile } from "./IProfile";

export interface IUserProfile {
    id: string;
    comment: Optional<string>;

    appUserId: string;
    appUser: IAppUser;

    profileId: string;
    profile: IProfile;
}

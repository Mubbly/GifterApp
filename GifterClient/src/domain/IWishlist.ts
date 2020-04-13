import { Optional } from "types/generalTypes";
import { IGift } from "./IGift";
import { IProfile } from "./IProfile";
import { IAppUser } from './IAppUser';

export interface IWishlist {
    id: string;
    comment: Optional<string>;

    appUserId: string;
    appUser: IAppUser;
}

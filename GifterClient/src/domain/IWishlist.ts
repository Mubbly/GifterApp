import { Optional } from "types/generalTypes";
import { IGift } from "./IGift";
import { IProfile } from "./IProfile";
import { IAppUser } from './IAppUser';
import { IBaseEntity } from "./base/IBaseEntity";

export interface IWishlistCreate {
    comment: Optional<string>;
}

export interface IWishlistEdit extends IBaseEntity, IWishlistCreate {
}

export interface IWishlist extends IWishlistEdit {
    appUserId: string;
    appUser: IAppUser;
}

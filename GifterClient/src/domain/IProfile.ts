import { Optional } from "types/generalTypes";
import { IAppUser } from "./IAppUser";
import { IWishlist } from './IWishlist';
import { IBaseEntity } from "./base/IBaseEntity";

export interface IProfileCreate {
    profilePicture: Optional<string>;
    gender: Optional<string>;
    bio: Optional<string>;
    age: Optional<number>;
    isPrivate: boolean;

    appUserId: string;
    wishlistId: string; // TODO
}

export interface IProfileEdit extends IBaseEntity, IProfileCreate {
}

export interface IProfile extends IProfileEdit {
    appUser: IAppUser;
    wishlist: IWishlist;

    userProfilesCount: number;
}

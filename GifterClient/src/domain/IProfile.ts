import { Optional } from "types/generalTypes";
import { IAppUser } from "./IAppUser";
import { IWishlist } from './IWishlist';

export interface IProfile {
    id: string;
    profilePicture: Optional<string>;
    gender: Optional<string>;
    bio: Optional<string>;
    age: Optional<number>;
    isPrivate: boolean;

    appUserId: string;
    appUser: IAppUser;

    wishlistId: string;
    wishlist: IWishlist;

    userProfilesCount: number;
}

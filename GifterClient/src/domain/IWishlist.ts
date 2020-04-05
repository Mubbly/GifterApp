import { Optional } from "types/generalTypes";
import { IGift } from "./IGift";
import { IProfile } from "./IProfile";

export interface IWishlist {
    id: string;
    comment: Optional<string>;

    giftId: string;
    gift: IGift;  // TODO: Should be the other way around

    profileId: string;
    profile: IProfile;
}

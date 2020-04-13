import { Optional } from "types/generalTypes";
import { IActionType } from "./IActionType";
import { IStatus } from "./IStatus";
import { IAppUser } from "./IAppUser";
import { IWishlist } from './IWishlist';

export interface IGift {
    id: string;
    name: string;
    description: Optional<string>;
    image: Optional<string>;
    url: Optional<string>;
    partnerUrl: Optional<string>;
    isPartnered: boolean;
    isPinned: boolean;

    actionTypeId: string;
    actionType: IActionType;

    statusId: string;
    status: IStatus;

    appUserId: string;
    appUser: IAppUser;

    wishlistId: string;
    wishlist: IWishlist;

    reservedGiftsCount: number;
    archivedGiftsCount: number;
}

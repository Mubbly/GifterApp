import { Optional } from "types/generalTypes";
import { IActionType } from "./IActionType";
import { IStatus } from "./IStatus";
import { IAppUser } from "./IAppUser";
import { IWishlist } from './IWishlist';
import { IBaseEntity } from "./base/IBaseEntity";

export interface IGiftCreate {
    name: string;
    description: Optional<string>;
    image: Optional<string>;
    url: Optional<string>;
    partnerUrl: Optional<string>;
    isPartnered: boolean;
    isPinned: boolean;

    actionTypeId: string;
    statusId: string;
    wishlistId: string;
}

export interface IGiftEdit extends IBaseEntity, IGiftCreate {
}

export interface IGift extends IGiftEdit {
    actionType: IActionType;
    status: IStatus;
    wishlist: IWishlist;

    reservedFrom?: Optional<string>;
    archivedFrom?: Optional<string>;
    isArchivalConfirmed?: Optional<string>;
    userGiverId?: Optional<string>;
    userGiverName?: Optional<string>;
    userReceiverId?: Optional<string>;
    userReceiverName?: Optional<string>;
    
    reservedGiftsCount: number;
    archivedGiftsCount: number;
}

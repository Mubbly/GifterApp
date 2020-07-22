import { Optional } from "types/generalTypes";
import { IActionType } from "./IActionType";
import { IStatus } from "./IStatus";
import { IGift } from "./IGift";
import { IAppUser } from "./IAppUser";
import { IBaseEntity } from "./base/IBaseEntity";

export interface IArchivedGiftCreate {
    isConfirmed: boolean;
    comment: Optional<string>;
    giftId: string;
    userGiverId: string;
    userReceiverId: string;
}

export interface IArchivedGiftResponse extends IArchivedGiftCreate {
    dateArchived: string;
}

export interface IArchivedGiftEdit extends IArchivedGiftCreate {
}

export interface IArchivedGiftPendingEdit {
    comment: Optional<string>;
    giftId: string;
    userGiverId: string;
}

/** TODO: Remove this temporary workaround to satisfy ArchivedGifts service */
export interface IArchivedGiftEditTemp extends IBaseEntity {} 

export interface IArchivedGift extends IBaseEntity, IArchivedGiftEdit {
    actionType: IActionType;
    status: IStatus;
    gift: IGift;
    userGiver: IAppUser;
    userReceiver: IAppUser;
}

export interface IArchivedGiftFull {
    dateArchived: string;
    comment: Optional<string>;

    actionTypeId: string;
    actionType: IActionType;
    statusId: string;
    status: IStatus;
    giftId: string;
    gift: IGift;
    userGiverId: string;
    userGiver: IAppUser;
    userReceiverId: string;
    userReceiver: IAppUser;
}

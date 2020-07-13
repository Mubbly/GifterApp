import { Optional } from "types/generalTypes";
import { IActionType } from "./IActionType";
import { IStatus } from "./IStatus";
import { IGift } from "./IGift";
import { IAppUser } from "./IAppUser";
import { IBaseEntity } from "./base/IBaseEntity";

export interface IReservedGiftCreate {
    comment: Optional<string>;
    giftId: string;
    userReceiverId: string;
}

export interface IReservedGiftEdit extends IReservedGiftCreate {}

/** TODO: Remove this temporary workaround to satisfy ReservedGifts service */
export interface IReservedGiftEditTemp extends IBaseEntity {} 

export interface IReservedGiftResponse extends IReservedGiftCreate {
    reservedFrom: string;
}

export interface IReservedGift extends IBaseEntity, IReservedGiftEdit {
    dateToSendReminder: string;

    actionType: IActionType;
    status: IStatus;
    gift: IGift;
    userGiver: IAppUser;
    userReceiver: IAppUser;
}

export interface IReservedGiftFull {
    reservedFrom: string;
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

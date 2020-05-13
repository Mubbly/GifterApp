import { Optional } from "types/generalTypes";
import { IActionType } from "./IActionType";
import { IStatus } from "./IStatus";
import { IGift } from "./IGift";
import { IAppUser } from "./IAppUser";
import { IBaseEntity } from "./base/IBaseEntity";

export interface IReservedGiftCreate {
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

export interface IReservedGiftEdit extends IBaseEntity, IReservedGiftCreate {
}

export interface IReservedGift extends IReservedGiftEdit {
    dateToSendReminder: string;

    actionType: IActionType;
    status: IStatus;
    gift: IGift;
    userGiver: IAppUser;
    userReceiver: IAppUser;
}

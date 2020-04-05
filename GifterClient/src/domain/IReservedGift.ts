import { Optional } from "types/generalTypes";
import { IActionType } from "./IActionType";
import { IStatus } from "./IStatus";
import { IGift } from "./IGift";
import { IAppUser } from "./IAppUser";

export interface IReservedGift {
    id: string;
    reservedFrom: string;
    comment: Optional<string>;
    dateToSendReminder: string;

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

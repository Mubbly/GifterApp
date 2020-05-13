import { Optional } from "types/generalTypes";
import { IActionType } from "./IActionType";
import { IStatus } from "./IStatus";
import { IGift } from "./IGift";
import { IAppUser } from "./IAppUser";
import { IBaseEntity } from "./base/IBaseEntity";

export interface IArchivedGiftCreate {
    dateArchived: string;
    isConfirmed: boolean;
    comment: Optional<string>;

    actionTypeId: string;
    statusId: string;
    giftId: string;
    userGiverId: string;
    userReceiverId: string;
}

export interface IArchivedGiftEdit extends IBaseEntity, IArchivedGiftCreate {
}

export interface IArchivedGift extends IArchivedGiftEdit {
    actionType: IActionType;
    status: IStatus;
    gift: IGift;
    userGiver: IAppUser;
    userReceiver: IAppUser;
}

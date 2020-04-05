import { Optional } from "types/generalTypes";
import { IActionType } from "./IActionType";
import { IStatus } from "./IStatus";
import { IGift } from "./IGift";
import { IAppUser } from "./IAppUser";

export interface IArchivedGift {
    id: string;
    dateArchived: string;
    isConfirmed: boolean;
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

import { Optional } from "types/generalTypes";
import { IActionType } from "./IActionType";
import { IStatus } from "./IStatus";
import { IAppUser } from "./IAppUser";

export interface IGiftCreate {
    name: string;
    description: Optional<string>;
    image: Optional<string>;
    url: Optional<string>;
    partnerUrl: Optional<string>;
    isPartnered: boolean;
    isPinned: boolean;
    actionTypeId: string;
    //actionType: string;
    statusId: string;
    //status: string;
    appUserId: string;
    //appUser: string;
}

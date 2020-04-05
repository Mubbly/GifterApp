import { Optional } from "types/generalTypes";
import { IActionType } from "./IActionType";
import { IStatus } from "./IStatus";
import { IAppUser } from "./IAppUser";

export interface IGiftEdit {
    id: string;
    name: string;
    description: Optional<string>;
    image: Optional<string>;
    url: Optional<string>;
    partnerUrl: Optional<string>;
    isPartnered: boolean;
    isPinned: boolean;
    actionTypeId: string;
    statusId: string;
    appUserId: string;
}

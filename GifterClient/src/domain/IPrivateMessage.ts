import { IAppUser } from "./IAppUser";
import { IBaseEntity } from "./base/IBaseEntity";

export interface IPrivateMessageCreate {
    message: string;
    sentAt: string;
    isSeen: boolean;
    userSenderId: string;
    userReceiverId: string;
}

export interface IPrivateMessageEdit extends IBaseEntity {
    message: string;
}

export interface IPrivateMessage extends IBaseEntity, IPrivateMessageCreate {
    userSender: IAppUser;
    userReceiver: IAppUser;
}

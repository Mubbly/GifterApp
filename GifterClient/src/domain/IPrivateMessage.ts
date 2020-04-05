import { IAppUser } from "./IAppUser";

export interface IPrivateMessage {
    id: string;
    message: string;
    sentAt: string;
    isSeen: boolean;

    userSenderId: string;
    userSender: IAppUser;

    userReceiverId: string;
    userReceiver: IAppUser;
}

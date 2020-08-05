import { IBaseEntity } from "./base/IBaseEntity";

export interface ICurrentUser extends IBaseEntity {
    firstName: string,
    lastName: string
}

export interface IAppUser extends IBaseEntity {
    email: string;
    userName: string;
    firstName: string;
    lastName: string;
    fullName: string;
    dateJoined: string;

    // userExamples: number;
    // etc
}

export interface IAppUserEdit extends IBaseEntity {
}

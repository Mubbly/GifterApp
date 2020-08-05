import { Optional } from "types/generalTypes";
import { IBaseEntity } from "./base/IBaseEntity";
import { IAppUser } from './IAppUser';

export interface IExampleCreate {
    name: string;
    description: Optional<string>;
}

export interface IExampleEdit extends IBaseEntity, IExampleCreate {
}

export interface IExample extends IExampleEdit {
    appUserId: string;
    appUser?: IAppUser;
}

// export interface IExampleResponse extends IExample {
// }

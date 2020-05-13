import { IBaseEntity } from "./base/IBaseEntity";

export interface IAppUser extends IBaseEntity {
    firstName: string;

    wishlistsCount: number;  // TODO: Should be the other way around
    reservedGiftsCount: number;
    archivedGiftsCount: number;
}

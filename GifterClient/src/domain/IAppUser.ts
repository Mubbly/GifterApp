export interface IAppUser {
    id: string;
    firstName: string;

    wishlistsCount: number;  // TODO: Should be the other way around
    reservedGiftsCount: number;
    archivedGiftsCount: number;
}

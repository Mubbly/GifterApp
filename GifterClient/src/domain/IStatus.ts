import { Optional } from 'types/generalTypes';
import { IBaseEntity } from './base/IBaseEntity';

export interface IStatusCreate {
    statusValue: string;
    comment: Optional<string>;
}

export interface IStatusEdit extends IBaseEntity, IStatusCreate {
}

export interface IStatus extends IStatusEdit {
    giftsCount: number;
    reservedGiftsCount: number;
    archivedGiftsCount: number;
    donateesCount: number;
}

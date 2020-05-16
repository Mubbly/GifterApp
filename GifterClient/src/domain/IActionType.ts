import { Optional } from 'types/generalTypes';
import { IBaseEntity } from './base/IBaseEntity';

export interface IActionTypeCreate {
    actionTypeValue: string;
    comment: Optional<string>;
}

export interface IActionTypeEdit extends IBaseEntity, IActionTypeCreate {
}

export interface IActionType extends IActionTypeEdit {
  giftsCount: number;
  reservedGiftsCount: number;
  archivedGiftsCount: number;
  donateesCount: number;
}


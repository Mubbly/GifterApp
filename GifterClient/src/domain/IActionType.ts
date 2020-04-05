import { Optional } from 'types/generalTypes';

export interface IActionType {
  id: string;
  actionTypeValue: string;
  comment: Optional<string>;

  giftsCount: number;
  reservedGiftsCount: number;
  archivedGiftsCount: number;
  donateesCount: number;
}

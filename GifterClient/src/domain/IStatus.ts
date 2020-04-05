import { Optional } from 'types/generalTypes';

export interface IStatus {
  id: string;
  statusValue: string;
  comment: Optional<string>;

  giftsCount: number;
  reservedGiftsCount: number;
  archivedGiftsCount: number;
  donateesCount: number;
}

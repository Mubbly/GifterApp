export interface IStatus {
  id: string;
  statusValue: string;
  comment: string | null;

  giftsCount: number;
  reservedGiftsCount: number;
  archivedGiftsCount: number;
  donateesCount: number;
}

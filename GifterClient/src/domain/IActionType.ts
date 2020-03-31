export interface IActionType {
  id: string;
  actionTypeValue: string;
  comment: string | null;

  giftsCount: number;
  reservedGiftsCount: number;
  archivedGiftsCount: number;
  donateesCount: number;
}

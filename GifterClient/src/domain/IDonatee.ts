import { IActionType } from './IActionType';
import { IStatus } from './IStatus';

export interface IDonatee {
  id: string;
  firstName: string;
  lastName: string| null;
  gender: string | null;
  age: number | null;
  bio: string | null;
  giftName: string;
  giftDescription: string | null;
  giftImage: string | null;
  giftUrl: string | null;
  giftReservedFrom: string | null;
  activeFrom: string;
  activeTo: string;
  isActive: boolean;

  actionTypeId: string;
  actionType: IActionType;

  statusId: string;
  status: IStatus;

  campaignDonateesCount: number;
}

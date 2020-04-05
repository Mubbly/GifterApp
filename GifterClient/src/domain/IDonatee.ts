import { IActionType } from './IActionType';
import { IStatus } from './IStatus';
import { Optional } from 'types/generalTypes';

export interface IDonatee {
  id: string;
  firstName: string;
  lastName: Optional<string>;
  gender: Optional<string>;
  age: Optional<number>;
  bio: Optional<string>;
  giftName: string;
  giftDescription: Optional<string>;
  giftImage: Optional<string>;
  giftUrl: Optional<string>;
  giftReservedFrom: Optional<string>;
  activeFrom: string;
  activeTo: string;
  isActive: boolean;

  actionTypeId: string;
  actionType: IActionType;

  statusId: string;
  status: IStatus;

  campaignDonateesCount: number;
}

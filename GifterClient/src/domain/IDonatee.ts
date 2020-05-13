import { IActionType } from './IActionType';
import { IStatus } from './IStatus';
import { Optional } from 'types/generalTypes';
import { IBaseEntity } from './base/IBaseEntity';

export interface IDonateeCreate {
    firstName: string;
    lastName: Optional<string>;
    gender: Optional<string>;
    age: Optional<number>;
    bio: Optional<string>;
    giftName: string;
    giftDescription: Optional<string>;
    giftImage: Optional<string>;
    giftUrl: Optional<string>;
    activeFrom: string;
    activeTo: string;

    actionTypeId: string;
    statusId: string;
}

export interface IDonateeEdit extends IBaseEntity, IDonateeCreate {
}

export interface IDonatee extends IDonateeEdit {
  isActive: boolean;
  giftReservedFrom: Optional<string>;

  actionType: IActionType;
  status: IStatus;

  campaignDonateesCount: number;
}

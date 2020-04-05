import { IActionType } from './IActionType';
import { IStatus } from './IStatus';
import { Optional } from 'types/generalTypes';

export interface IDonateeEdit {
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
    activeFrom: string;
    activeTo: string;
    actionTypeId: string;
    statusId: string;
}

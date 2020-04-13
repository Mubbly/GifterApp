import { Optional } from 'types/generalTypes';

export interface IActionTypeEdit {
  id: string;
  actionTypeValue: string;
  comment: Optional<string>;
}

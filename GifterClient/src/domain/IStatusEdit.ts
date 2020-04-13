import { Optional } from 'types/generalTypes';

export interface IStatusEdit {
  id: string;
  statusValue: string;
  comment: Optional<string>;
}

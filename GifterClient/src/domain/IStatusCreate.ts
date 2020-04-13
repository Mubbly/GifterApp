import { Optional } from 'types/generalTypes';

export interface IStatusCreate {
  statusValue: string;
  comment: Optional<string>;
}

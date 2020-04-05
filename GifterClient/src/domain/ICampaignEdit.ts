import { Optional } from 'types/generalTypes';

export interface ICampaignEdit {
    id: string;
    name: string;
    description: Optional<string>;
    adImage: Optional<string>;
    institution: Optional<string>;
    activeFromDate: string;
    activeToDate: string;
}

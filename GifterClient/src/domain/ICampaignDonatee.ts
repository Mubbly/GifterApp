import { ICampaign } from './ICampaign';
import { IDonatee } from './IDonatee';
import { Optional } from 'types/generalTypes';

export interface ICampaignDonatee {
    id: string;
    isActive: boolean;
    comment: Optional<string>;

    campaignId: string;
    campaign: ICampaign;

    donateeId: string;
    donatee: IDonatee;
}

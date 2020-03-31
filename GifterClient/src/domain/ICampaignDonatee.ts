import { ICampaign } from './ICampaign';
import { IDonatee } from './IDonatee';

export interface ICampaignDonatee {
    id: string;
    isActive: boolean;
    comment: string | null;

    campaignId: string;
    campaign: ICampaign;

    donateeId: string;
    donatee: IDonatee;
}

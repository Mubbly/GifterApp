import { Optional } from 'types/generalTypes';

export interface ICampaign {
    id: string;
    name: string;
    description: Optional<string>;
    adImage: Optional<string>;
    institution: Optional<string>;
    activeFromDate: string;
    activeToDate: string;
    isActive: boolean;

    userCampaignsCount: number;
    campaignDonateesCount: number;
}

import { Optional } from 'types/generalTypes';
import { IBaseEntity } from './base/IBaseEntity';

export interface ICampaignCreate {
    name: string;
    description: Optional<string>;
    adImage: Optional<string>;
    institution: Optional<string>;
    activeFromDate: string;
    activeToDate: string;
}

export interface ICampaignEdit extends IBaseEntity, ICampaignCreate {
}

export interface ICampaign extends ICampaignEdit {
    isActive: boolean;

    userCampaignsCount: number;
    campaignDonateesCount: number;
}

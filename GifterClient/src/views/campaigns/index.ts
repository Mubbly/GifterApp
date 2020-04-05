import { autoinject } from 'aurelia-framework';
import { ICampaign } from 'domain/ICampaign';
import { CampaignService } from 'service/campaignService';

@autoinject
export class CampaignsIndex {
    private _campaigns: ICampaign[] = [];

    constructor(private campaignService: CampaignService) {

    }

    attached() {
        this.campaignService.getCampaigns().then(
            data => this._campaigns = data
        );
    }
}

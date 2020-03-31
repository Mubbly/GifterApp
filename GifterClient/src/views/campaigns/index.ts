import { autoinject } from 'aurelia-framework';
import { CampaignService } from '../../service/campaign-service';
import { ICampaign } from '../../domain/ICampaign';

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

import { autoinject } from 'aurelia-framework';
import { ICampaignDonatee } from 'domain/ICampaignDonatee';
import { CampaignDonateeService } from 'service/campaignDonateeService';

@autoinject
export class CampaignDonateesIndex {
    private _campaignDonatees: ICampaignDonatee[] = [];

    constructor(private campaignDonateeService: CampaignDonateeService) {

    }

    attached() {
        this.campaignDonateeService.getCampaignDonatees().then(
            data => this._campaignDonatees = data
        );
    }
}

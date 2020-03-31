import { autoinject } from 'aurelia-framework';
import { CampaignDonateeService } from '../../service/campaigndonatee-service';
import { ICampaignDonatee } from '../../domain/ICampaignDonatee';

@autoinject
export class CampaignDonateesIndex {
    private _campaignDonatees: ICampaignDonatee[] = [];

    constructor(private CampaignDonateeService: CampaignDonateeService) {

    }

    attached() {
        this.CampaignDonateeService.getCampaignDonatees().then(
            data => this._campaignDonatees = data
        );
    }
}

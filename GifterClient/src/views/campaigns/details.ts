import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { ICampaign } from 'domain/ICampaign';
import { CampaignService } from 'service/campaignService';

@autoinject
export class CampaignDetails {
    private _campaigns: ICampaign[] = [];
    private _campaign: Optional<ICampaign> = null;

    constructor(private campaignService: CampaignService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(params.id && typeof(params.id) === 'string') {
            this.campaignService.getCampaign(params.id).then(
                data => this._campaign = data
            )
        }
    }
}

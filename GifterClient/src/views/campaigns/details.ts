import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { ICampaign } from 'domain/ICampaign';
import { CampaignService } from 'service/campaignService';
import * as UtilFunctions from 'utils/utilFunctions';
import { PermissionDetails } from '../permissions/details';

@autoinject
export class CampaignDetails {
    private _campaign: Optional<ICampaign> = null;

    constructor(private campaignService: CampaignService) {

    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        this.getCampaign(params.id);
    }

    private getCampaign(id: string): void {
        if(UtilFunctions.existsAndIsString(id)) {
            this.campaignService.getCampaign(id).then(
                response => {
                    if(UtilFunctions.isSuccessful(response)) {
                        this._campaign = response.data!;
                    } else {
                        UtilFunctions.alertErrorMessage(response);
                    }
                }
            )
        }
    }
}

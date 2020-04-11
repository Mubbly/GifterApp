import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { CampaignService } from 'service/campaignService';
import * as UtilFunctions from 'utils/utilFunctions';
import { ICampaign } from 'domain/ICampaign';
import { Optional } from 'types/generalTypes';

@autoinject
export class CampaignsDelete {
    private _campaign?: ICampaign;

    private _errorMessage: Optional<string> = null;

    constructor(private campaignService: CampaignService, private router: Router) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        const campaignId = params.id;
        this.getCampaign(campaignId);
    }

    onSubmit(event: Event) {
        this.deleteCampaign(this._campaign!.id);
        event.preventDefault();
    }

    private getCampaign(id: string) {
        if(UtilFunctions.existsAndIsString(id)) {
            this.campaignService.getCampaign(id).then(
                response => {
                    if(UtilFunctions.isSuccessful(response)) {
                        this._campaign = response.data!;
                    } else {
                        this._errorMessage = UtilFunctions.getErrorMessage(response);
                    }
                }
            )
        }
    }

    private deleteCampaign(id: string) {
        this.campaignService
        .deleteCampaign(id)
        .then(
            response => {
                if (UtilFunctions.isSuccessful(response)) {
                    this.router.navigateToRoute('campaignsIndex', {});
                } else {
                    this._errorMessage = UtilFunctions.getErrorMessage(response);
                }
            }
        );
    }
}

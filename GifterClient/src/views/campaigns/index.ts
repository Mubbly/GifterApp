import { Router } from "aurelia-router";
import { autoinject } from "aurelia-framework";
import { ICampaign } from "domain/ICampaign";
import { CampaignService } from "service/campaignService";
import * as Utils from "utils/utilFunctions";
import { Optional, GifterInterface } from "types/generalTypes";
import { IFetchResponse } from '../../types/IFetchResponse';
import { AppState } from "state/appState";
import { CampaignsPersonal } from './personal';

@autoinject
export class CampaignsIndex {
    private _campaigns: ICampaign[] = [];
    private _isCampaignManager: boolean = false;
    private _errorMessage: Optional<string> = null;

    constructor(
        private campaignService: CampaignService,
        private campaignsPersonal: CampaignsPersonal,
        private router: Router,
        private appState: AppState
    ) {}

    attached() {
    }

    activate() {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getCampaigns();
        }
    }
    
    private getCampaigns(): void {
        this.campaignService
        .getAll()
        .then((response) => {
            if (!Utils.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                this._campaigns = response.data!;

                this._campaigns.forEach(campaign => {
                    campaign.activeFromDate = Utils.formatAsHtml5Date(campaign.activeFromDate);
                    campaign.activeToDate = Utils.formatAsHtml5Date(campaign.activeFromDate);
                });
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    /**
     * Set error message or route to login/home page
     */
    private handleErrors(response: IFetchResponse<GifterInterface | GifterInterface[]>) {
        switch(response.status) {
            case Utils.STATUS_CODE_UNAUTHORIZED:
                this.router.navigateToRoute(Utils.LOGIN_ROUTE);
                break;
            default:
                this._errorMessage = Utils.getErrorMessage(response);
        }
    }
}

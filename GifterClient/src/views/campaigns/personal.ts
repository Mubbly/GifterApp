import { Router } from "aurelia-router";
import { autoinject } from "aurelia-framework";
import { ICampaign } from "domain/ICampaign";
import { CampaignService } from "service/campaignService";
import * as Utils from "utils/utilFunctions";
import { Optional, GifterInterface } from "types/generalTypes";
import { IFetchResponse } from '../../types/IFetchResponse';
import { AppState } from "state/appState";

@autoinject
export class CampaignsPersonal {
    private readonly ERROR_NOT_CAMPAIGN_MANAGER = "You have to be a campaign manager to create new campaigns";

    private _campaigns: ICampaign[] = [];
    private _errorMessage: Optional<string> = null;
    private _isCampaignManager = false;

    constructor(
        private campaignService: CampaignService,
        private router: Router,
        private appState: AppState
    ) {}

    attached() {}

    activate() {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getAllCampaigns();
        }
    }
    
    private getAllCampaigns(): void {
        this.campaignService
        .getPersonalCampaigns()
        .then((response) => {
            if (!Utils.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                this._isCampaignManager = true;
                this._campaigns = response.data!;
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
            case Utils.STATUS_CODE_FORBIDDEN:
                this._isCampaignManager = false;
                this._errorMessage = this.ERROR_NOT_CAMPAIGN_MANAGER;
                break;
            default:
                this._errorMessage = Utils.getErrorMessage(response);
        }
    }
}
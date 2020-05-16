import { Router } from 'aurelia-router';
import { autoinject } from "aurelia-framework";
import { IDonatee } from "domain/IDonatee";
import { DonateeService } from "service/donateeService";
import * as Utils from "utils/utilFunctions";
import { Optional, GifterInterface } from "types/generalTypes";
import { ICampaign } from "domain/ICampaign";
import { CampaignService } from "service/campaignService";
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';

@autoinject
export class DonateesIndex {
    private readonly ERROR_NOT_VALID_CAMPAIGN_ID = "Selected campaign is not valid";
    private readonly ERROR_NO_DONATEES_FOR_THIS_CAMPAIGN = "There are no donatees for this campaign";

    private _donatees: IDonatee[] = [];
    private _campaign: Optional<ICampaign> = null;
    private _errorMessage: Optional<string> = null;

    constructor(
        private donateeService: DonateeService, 
        private campaignService: CampaignService, 
        private router: Router, 
        private appState: AppState) {}

    attached() {}

    activate(params: any) {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getCampaignDonatees(params.campaignId);
        }
    }

    private getCampaignDonatees(campaignId: string): void {
        if(!campaignId) {
            this._errorMessage = this.ERROR_NOT_VALID_CAMPAIGN_ID;
        }
        this.donateeService
        .getAll()
        .then((response) => {
            if (!Utils.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                this._donatees = response.data!;
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    private getDonatees(): void {
        this.donateeService
        .getAll()
        .then((response) => {
            if (!Utils.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                this._donatees = response.data!;
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
            case Utils.STATUS_CODE_NOT_FOUND:
                this._errorMessage = this.ERROR_NO_DONATEES_FOR_THIS_CAMPAIGN;
                break;
            default:
                this._errorMessage = Utils.getErrorMessage(response);
        }
    }
}

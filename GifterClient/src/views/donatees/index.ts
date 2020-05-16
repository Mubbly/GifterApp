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

    private _donatees: IDonatee[] = [];
    private _campaignName: string = '';
    private _campaignId: string = '';
    public _ownsCurrentCampaign = false;
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
            this._campaignId = params.campaignId;
            this._campaignName = params.campaignName;

            console.log(this._campaignId);
            console.log(this._campaignName);

            this.getCampaignDonatees(this._campaignId);
        }
    }

    private getCampaignDonatees(campaignId: string): void {
        // Cannot get donatees for a certain Campaign without its ID
        if(!campaignId || campaignId == '') {
            this._errorMessage = this.ERROR_NOT_VALID_CAMPAIGN_ID;
            return;
        }
        // Check if current user owns this campaign
        this.campaignService
            .getPersonal(campaignId)
            .then((response) => {
            if(Utils.isSuccessful(response)) {
                this._ownsCurrentCampaign = true;
            } else {
                this._ownsCurrentCampaign = false;
            }
        });

        // Get donatees for this campaign
        this.donateeService
            .getAllForCampaign(campaignId)
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

    // private getDonatees(): void {
    //     this.donateeService
    //         .getAll()
    //         .then((response) => {
    //             if (!Utils.isSuccessful(response)) {
    //                 this.handleErrors(response);
    //             } else {
    //                 this._donatees = response.data!;
    //             }
    //         })
    //         .catch((error) => {
    //             console.log(error);
    //         });
    // }
    
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

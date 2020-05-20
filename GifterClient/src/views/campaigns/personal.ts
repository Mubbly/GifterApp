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
    private readonly ERROR_NOT_CAMPAIGN_MANAGER = "You need to be a campaign manager to create new campaigns";

    private _campaigns: ICampaign[] = [];
    private _campaign: Optional<ICampaign> = null;
    private _errorMessage: Optional<string> = null;
    public _isCampaignManager = false;

    constructor(
        private campaignService: CampaignService,
        private router: Router,
        private appState: AppState
    ) {
    }

    attached() {}

    activate() {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getAllPersonalCampaigns();
        }
    }
    
    /**
     * Get campaigns that the user has created
     */
    private getAllPersonalCampaigns(): void {
        this.campaignService
        .getAllPersonal()
        .then((response) => {
            if (!Utils.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                this.setCampaignManager(true);
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
     * Get a campaign that the user has created
     */
    private getPersonalCampaign(id: string): void {
        this.campaignService
        .getPersonal(id)
        .then((response) => {
            if (!Utils.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                this.setCampaignManager(true);
                this._campaign = response.data!;

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
            case Utils.STATUS_CODE_FORBIDDEN:
                this.setCampaignManager(false);
                break;
            default:
                this._errorMessage = Utils.getErrorMessage(response);
        }
    }

    /** 
     * Sets _isCampaignManager and _errorMessage based on param. 
     * HTML view depends on it 
     */
    private setCampaignManager(isCampaignManager: boolean) {
        this._isCampaignManager = isCampaignManager;
        this._errorMessage = isCampaignManager ? null : this.ERROR_NOT_CAMPAIGN_MANAGER;
    }
}

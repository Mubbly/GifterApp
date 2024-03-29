import { autoinject } from "aurelia-framework";
import { RouteConfig, NavigationInstruction, Router } from "aurelia-router";
import { Optional, GifterInterface } from "types/generalTypes";
import { ICampaign } from "domain/ICampaign";
import { CampaignService } from "service/campaignService";
import * as Utils from "utils/utilFunctions";
import { IFetchResponse } from "types/IFetchResponse";
import { AppState } from "state/appState";

@autoinject
export class CampaignDetails {
    private _campaign: Optional<ICampaign> = null;
    private _errorMessage: Optional<string> = null;
    private _isCampaignManager = false;

    constructor(
        private campaignService: CampaignService,
        private router: Router,
        private appState: AppState
    ) {}

    attached() {}

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        const isLoggedIn = this.appState.jwt;
        if(!isLoggedIn) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getCampaign(params.id);
        }
    }

    private getCampaign(id: string): void {
        if (Utils.existsAndIsString(id)) {
            this.campaignService.get(id).then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this.handleErrors(response);
                } else {
                    this._campaign = response.data!;

                    this._campaign.activeFromDate = Utils.formatAsHtml5Date(this._campaign.activeFromDate);
                    this._campaign.activeToDate = Utils.formatAsHtml5Date(this._campaign.activeToDate);
                }
            });
        }
    }

    /** 
     * Sets _isCampaignManager based on param
     * HTML view depends on it 
     */
    private setCampaignManager(isCampaignManager: boolean) {
        this._isCampaignManager = isCampaignManager;
    }

    /**
     * Set error message or route to login/home page
     */
    private handleErrors(response: IFetchResponse<GifterInterface | GifterInterface[]>) {
        switch (response.status) {
            case Utils.STATUS_CODE_UNAUTHORIZED:
                this.router.navigateToRoute(Utils.LOGIN_ROUTE);
                break;
            default:
                this._errorMessage = Utils.getErrorMessage(response);
        }
    }
}

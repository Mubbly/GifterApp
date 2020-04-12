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

    constructor(
        private campaignService: CampaignService,
        private router: Router,
        private appState: AppState
    ) {}

    attached() {}

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getCampaign(params.id);
        }
    }

    private getCampaign(id: string): void {
        if (Utils.existsAndIsString(id)) {
            this.campaignService.getCampaign(id).then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this.handleErrors(response);
                } else {
                    this._campaign = response.data!;
                }
            });
        }
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

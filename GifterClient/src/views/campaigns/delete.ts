import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { CampaignService } from 'service/campaignService';
import * as Utils from 'utils/utilFunctions';
import { ICampaign } from 'domain/ICampaign';
import { Optional, GifterInterface } from 'types/generalTypes';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';

@autoinject
export class CampaignsDelete {
    private readonly CAMPAIGNS_ROUTE = 'campaignsIndex';
    private readonly ERROR_NOT_CAMPAIGN_MANAGER = "You have to be a campaign manager to create new campaigns";

    private _campaign?: ICampaign;
    private _errorMessage: Optional<string> = null;
    private _isCampaignManager = false;

    constructor(private campaignService: CampaignService, private router: Router, private appState: AppState) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        const isLoggedIn = this.appState.jwt;
        if(!isLoggedIn) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getCampaign(params.id);
        }
    }

    onSubmit(event: Event) {
        this.deleteCampaign(this._campaign!.id);
        event.preventDefault();
    }

    private getCampaign(id: string): void {
        if (Utils.existsAndIsString(id)) {
            this.campaignService.getCampaign(id).then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this.handleErrors(response);
                } else {
                    this.setCampaignManager(true);
                    this._campaign = response.data!;
                }
            });
        }
    }

    private deleteCampaign(id: string) {
        this.campaignService
        .deleteCampaign(id)
        .then(
            response => {
                if (!Utils.isSuccessful(response)) {
                    this._errorMessage = Utils.getErrorMessage(response);
                } else {
                    this.router.navigateToRoute(this.CAMPAIGNS_ROUTE, {});
                }
            }
        );
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

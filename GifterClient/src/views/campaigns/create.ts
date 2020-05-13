import { autoinject } from "aurelia-framework";
import { RouteConfig, NavigationInstruction, Router } from "aurelia-router";
import { CampaignService } from "service/campaignService";
import * as Utils from "utils/utilFunctions";
import { ICampaignCreate } from "domain/ICampaign";
import { Optional } from "types/generalTypes";
import { AppState } from "state/appState";
import { App } from "app";
import { IFetchResponse } from "types/IFetchResponse";

@autoinject
export class CampaignsCreate {
    private _campaign?: ICampaignCreate;
    private _name = "";
    private _activeToDate = "";
    private _activeFromDate = "";
    private _description = null;
    private _adImage = null;
    private _institution = null;
    private _errorMessage: Optional<string> = null;
    private _isCampaignManager = false;
    private _jwt: boolean = false;

    constructor(
        private campaignService: CampaignService,
        private router: Router,
        private appState: AppState,
    ) {}

    attached() {}

    activate() {
        const isLoggedIn = this.appState.jwt;
        if(!isLoggedIn) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        }
        this._isCampaignManager = true; // TODO: Create logic for this
    }

    onSubmit(event: Event) {
        let newCampaign: ICampaignCreate = {
            name: this._name,
            description: this._description,
            adImage: this._adImage,
            institution: this._institution,
            activeFromDate: this._activeFromDate,
            activeToDate: this._activeToDate,
        };
        this.createCampaign(newCampaign);

        event.preventDefault();
    }

    private createCampaign(newCampaign: ICampaignCreate) {
        this.campaignService
            .create(newCampaign)
            .then((response: IFetchResponse<ICampaignCreate>) => {
                if (!Utils.isSuccessful(response)) {
                    this._errorMessage = Utils.getErrorMessage(response);
                } else {
                    this.router.navigateToRoute("campaignsIndex", {});
                }
            });
    }
}

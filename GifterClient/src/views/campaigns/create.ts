import { autoinject } from "aurelia-framework";
import { RouteConfig, NavigationInstruction, Router } from "aurelia-router";
import { CampaignService } from "service/campaignService";
import * as Utils from "utils/utilFunctions";
import { ICampaignCreate } from "domain/ICampaignCreate";
import { Optional } from "types/generalTypes";
import { AppState } from "state/appState";
import { App } from "app";

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
    private _jwt: boolean = false;

    constructor(
        private campaignService: CampaignService,
        private router: Router,
        private appState: AppState,
    ) {}

    attached() {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        }
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
        this.campaignService.createCampaign(newCampaign).then((response) => {
            if (!Utils.isSuccessful(response)) {
                this._errorMessage = Utils.getErrorMessage(response);
            } else {
                this.router.navigateToRoute("campaignsIndex", {});
            }
        });
    }
}

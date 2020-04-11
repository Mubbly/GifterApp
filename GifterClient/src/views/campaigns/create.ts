import { autoinject } from "aurelia-framework";
import { RouteConfig, NavigationInstruction, Router } from "aurelia-router";
import { CampaignService } from "service/campaignService";
import { AppUserService } from "service/appUserService";
import { ICampaignEdit } from "domain/ICampaignEdit";
import * as UtilFunctions from "utils/utilFunctions";
import { IAppUser } from "domain/IAppUser";
import { IStatus } from "domain/IStatus";
import { IActionType } from "domain/IActionType";
import { StatusService } from "service/statusService";
import { ActionTypeService } from "service/actionTypeService";
import { ICampaignCreate } from "domain/ICampaignCreate";
import { Optional } from "types/generalTypes";

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

    constructor(private campaignService: CampaignService, private router: Router) {

    }

    attached() {}

    onSubmit(event: Event) {
        let newCampaign: ICampaignCreate = {
            name: this._name,
            description: this._description,
            adImage: this._adImage,
            institution: this._institution,
            activeFromDate: this._activeFromDate,
            activeToDate: this._activeToDate
        };
        console.log(newCampaign);
        this.createCampaign(newCampaign);
        
        event.preventDefault();
    }

    private createCampaign(newCampaign: ICampaignCreate) {
        this.campaignService
        .createCampaign(newCampaign)
        .then((response) => {
            if (UtilFunctions.isSuccessful(response)) {
                this.router.navigateToRoute("campaignsIndex", {});
            } else {
                this._errorMessage = UtilFunctions.getErrorMessage(response);
            }
        });
    }
}

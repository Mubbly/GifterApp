import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { ICampaignEdit } from 'domain/ICampaign';
import * as Utils from 'utils/utilFunctions';
import { Optional, GifterInterface, HTML5DateString } from 'types/generalTypes';
import { CampaignService } from 'service/campaignService';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';

@autoinject
export class CampaignsEdit {
    private readonly CAMPAIGN_ROUTE = 'campaignsIndex';
    private readonly ERROR_NOT_CAMPAIGN_MANAGER = "You have to be a campaign manager to create new campaigns";
    private readonly ERROR_REQUIRED_FIELDS = "Please fill in required fields!";

    private _campaign?: ICampaignEdit;

    private _errorMessage: Optional<string> = null;
    private _isCampaignManager = false;

    constructor(private campaignService: CampaignService, private router: Router, private appState: AppState) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getCampaign(params.id);
        }
    }

    onSubmit(event: Event) {
        this.getNewValuesFromInputs();
        this.updateCampaign();
        
        event.preventDefault();
    }

    /** Reassigns _campaign props */
    private getNewValuesFromInputs() {
        let nameInput = <string>this._campaign!.name;
        let activeToInput = <string>this._campaign!.activeToDate;
        let activeFromInput = <string>(this._campaign!.activeFromDate);

        if(Utils.isNullOrEmpty(nameInput) || Utils.isNullOrEmpty(activeToInput) || Utils.isNullOrEmpty(activeFromInput)) {
            this._errorMessage = this.ERROR_REQUIRED_FIELDS;
            return;
        }
        let descInput = <Optional<string>>this._campaign!.description;
        let adImageInput = <Optional<string>>this._campaign!.adImage;
        let institutionInput = <Optional<string>>this._campaign!.institution;

        this._campaign!.description = Utils.isNullOrEmpty(descInput) ? null : descInput;
        this._campaign!.adImage = Utils.isNullOrEmpty(adImageInput) ? null : adImageInput;
        this._campaign!.institution = Utils.isNullOrEmpty(institutionInput) ? null : institutionInput;

        console.log(this._campaign);
    }

    private updateCampaign(): void {
        this.campaignService
            .update(this._campaign!)
            .then(
                (response: IFetchResponse<ICampaignEdit>) => {
                    if (Utils.isSuccessful(response)) {
                        this.router.navigateToRoute(this.CAMPAIGN_ROUTE, {});
                    } else {
                        this._errorMessage = Utils.getErrorMessage(response);
                    }
                }
            );
    }

    private getCampaign(id: string): void {
        if (Utils.existsAndIsString(id)) {
            this.campaignService.get(id).then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this.handleErrors(response);
                } else {
                    this.setCampaignManager(true);
                    this._campaign = response.data!;
                    this._campaign.activeFromDate = Utils.formatAsHtml5Date(this._campaign!.activeFromDate);
                    this._campaign.activeToDate = Utils.formatAsHtml5Date(this._campaign!.activeToDate);
                }
            });
        }
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

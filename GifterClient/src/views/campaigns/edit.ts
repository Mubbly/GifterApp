import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { ICampaignEdit } from 'domain/ICampaignEdit';
import * as Utils from 'utils/utilFunctions';
import { Optional, GifterInterface, HTML5DateString } from 'types/generalTypes';
import { CampaignService } from 'service/campaignService';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';

@autoinject
export class CampaignsEdit {
    private readonly CAMPAIGN_ROUTE = 'campaignsIndex';
    private readonly ERROR_REQUIRED_FIELDS = "Please fill in required fields!";

    private _campaign?: ICampaignEdit;

    private _errorMessage: Optional<string> = null;

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
        this.setNewValuesToFields();
        this.updateCampaign();
        
        event.preventDefault();
    }

    private setNewValuesToFields() {
        let nameInput = <string>this._campaign!.name;
        let activeToInput = <string>this._campaign!.activeToDate;
        let activeFromInput = <string>(this._campaign!.activeFromDate);

        if(Utils.isEmpty(nameInput) || Utils.isEmpty(activeToInput) || Utils.isEmpty(activeFromInput)) {
            this._errorMessage = this.ERROR_REQUIRED_FIELDS;
            return;
        }
        let descInput = <Optional<string>>this._campaign!.description;
        let adImageInput = <Optional<string>>this._campaign!.adImage;
        let institutionInput = <Optional<string>>this._campaign!.institution;

        this._campaign!.description = Utils.isEmpty(descInput) ? null : descInput;
        this._campaign!.adImage = Utils.isEmpty(adImageInput) ? null : adImageInput;
        this._campaign!.institution = Utils.isEmpty(institutionInput) ? null : institutionInput;

        console.log(this._campaign);
    }

    private updateCampaign(): void {
        this.campaignService
            .updateCampaign(this._campaign!)
            .then(
                response => {
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
            this.campaignService.getCampaign(id).then((response) => {
                if (!Utils.isSuccessful(response)) {
                    this.handleErrors(response);
                } else {
                    this._campaign = response.data!;
                    this._campaign.activeFromDate = this.formatAsHtml5Date(this._campaign!.activeFromDate);
                    this._campaign.activeToDate = this.formatAsHtml5Date(this._campaign!.activeToDate);
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
            default:
                this._errorMessage = Utils.getErrorMessage(response);
        }
    }

    /**
     * Returns date as YYYY-MM-DD (or the same unedited date if parsing fails)
     * so that html5 date input value could be filled in
     * TODO: GET RID OF THIS STUPID TEMPORARY WORKAROUND AND USE SOME LIBRARY!
     */
    private formatAsHtml5Date(date: string): HTML5DateString | string {
        if(Utils.existsAndIsString(date)) {
            let newDate: Date = new Date();

            try {
               newDate = new Date(date);
               if(newDate) {
                    let year = newDate.getFullYear();
                    let month: number | string = newDate.getMonth() + 1; // +1 because january is 0
                    month = month < 10 ? `0${month}` : month; // add 0 before one digit numbers
                    let day: number | string = newDate.getDate();
                    day = day < 10 ? `0${day}` : day; // add 0 before one digit numbers
            
                    let html5Date = `${year}-${month}-${day}`;
                    return html5Date;
               }
            } catch(error) {
                console.warn(`Could not parse given date (${date}) into html5 format: ${error}`);
                return date;
            }
        }
        console.warn(`Could not parse given date (${date}) into html5 format`);
        return date;
    }
}

import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { ICampaignEdit } from 'domain/ICampaignEdit';
import * as UtilFunctions from 'utils/utilFunctions';
import { Optional } from 'types/generalTypes';
import { CampaignService } from '../../service/campaignService';
import { PermissionDetails } from '../permissions/details';

@autoinject
export class CampaignsEdit {
    private _campaign?: ICampaignEdit;

    constructor(private campaignService: CampaignService, private router: Router) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        const campaignId = params.id;
        this.getCampaign(campaignId);
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

        if(UtilFunctions.isEmpty(nameInput) || UtilFunctions.isEmpty(activeToInput) || UtilFunctions.isEmpty(activeFromInput)) {
            alert("Name or dates missing!")
            return;
        }
        let descInput = <Optional<string>>this._campaign!.description;
        let adImageInput = <Optional<string>>this._campaign!.adImage;
        let institutionInput = <Optional<string>>this._campaign!.institution;

        this._campaign!.description = (descInput === null || descInput.length === 0) ? null : descInput;
        this._campaign!.adImage = (adImageInput === null || adImageInput.length === 0) ? null : adImageInput;
        this._campaign!.institution = (institutionInput === null || institutionInput.length === 0) ? null : institutionInput;

        console.log(this._campaign);
    }

    private updateCampaign(): void {
        this.campaignService
            .updateCampaign(this._campaign!)
            .then(
                response => {
                    if (UtilFunctions.isSuccessful(response)) {
                        this.router.navigateToRoute('campaignsIndex', {});
                    } else {
                        UtilFunctions.alertErrorMessage(response);
                    }
                }
            );
    }

    private getCampaign(id: string): void {
        if(UtilFunctions.existsAndIsString(id)) {
            this.campaignService.getCampaign(id).then(
                response => {
                    if(UtilFunctions.isSuccessful(response)) {
                        this._campaign = response.data!;
                    } else {
                        UtilFunctions.alertErrorMessage(response);
                    }
                }
            )
        }
    }
}

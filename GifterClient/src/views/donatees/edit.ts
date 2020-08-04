import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { DonateeService } from 'service/donateeService';
import { IDonateeEdit } from 'domain/IDonatee';
import * as UtilFunctions from 'utils/utilFunctions';
import { Optional } from 'types/generalTypes';
import { IFetchResponse } from 'types/IFetchResponse';
import { ActionTypeService } from 'service/actionTypeService';
import { StatusService } from 'service/statusService';
import { IActionType } from 'domain/IActionType';
import { IStatus } from 'domain/IStatus';
import * as Utils from 'utils/utilFunctions';

@autoinject
export class DonateesEdit {
    private _donatee?: IDonateeEdit;
    private _actionTypes: IActionType[] = [];
    private _statuses: IStatus[] = [];
    private _campaignId: string = '';
    private _campaignName: string = '';
    private _errorMessage: Optional<string> = null;

    constructor(private donateeService: DonateeService,
        private actionTypeService: ActionTypeService,
        private statusService: StatusService, 
        private router: Router) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        const donateeId = params.id;
        this._campaignId = params.campaignId;
        this._campaignName = params.campaignName;

        if(UtilFunctions.existsAndIsString(donateeId)) {
            this.getRelatedData();

            this.donateeService.get(donateeId).then(
                response => {
                    if(UtilFunctions.isSuccessful(response)) {
                        this._donatee = response.data!;
                        this._donatee!.activeFrom = Utils.formatAsHtml5Date(this._donatee.activeFrom);
                        this._donatee!.activeTo = Utils.formatAsHtml5Date(this._donatee.activeTo);
                    } else {
                        this._errorMessage = UtilFunctions.getErrorMessage(response);

                    }
                }
            )
        }
    }

    // From other tables that are connected to this one via foreign keys
    private getRelatedData() {
        this.actionTypeService.getAll().then(
            result => this._actionTypes = result.data!
        );
        this.statusService.getAll().then(
            result => this._statuses = result.data!
        );
    }

    onSubmit(event: Event) {
        let firstNameInput = <string>this._donatee!.firstName;
        let giftNameInput = <string>this._donatee!.giftName;
        let activeFromInput = <string>this._donatee!.activeFrom;
        let activeToInput = <string>this._donatee!.activeTo;

        if(UtilFunctions.isNullOrEmpty(firstNameInput) 
            || UtilFunctions.isNullOrEmpty(giftNameInput)
            || UtilFunctions.isNullOrEmpty(activeFromInput)
            || UtilFunctions.isNullOrEmpty(activeToInput)) {
            alert("Names or dates missing!")
            return;
        }
        let lastNameInput = <Optional<string>>this._donatee!.lastName;
        let genderInput = <Optional<string>>this._donatee!.gender;
        let ageInput = <Optional<string>>this._donatee!.age;
        let bioInput = <Optional<string>>this._donatee!.bio;
        let giftDescriptionInput = <Optional<string>>this._donatee!.giftDescription;
        let giftImageInput = <Optional<string>>this._donatee!.giftImage;
        let giftUrlInput = <Optional<string>>this._donatee!.giftUrl;

        this._donatee!.lastName = UtilFunctions.isNullOrEmpty(lastNameInput) ? null : lastNameInput;
        this._donatee!.gender = UtilFunctions.isNullOrEmpty(genderInput) ? null : genderInput;
        this._donatee!.age = UtilFunctions.isNullOrEmpty(ageInput) ? null : Number(ageInput);
        this._donatee!.bio = UtilFunctions.isNullOrEmpty(bioInput) ? null : bioInput;
        this._donatee!.giftDescription = UtilFunctions.isNullOrEmpty(giftDescriptionInput) ? null : giftDescriptionInput;
        this._donatee!.giftImage = UtilFunctions.isNullOrEmpty(giftImageInput) ? null : giftImageInput;
        this._donatee!.giftUrl = UtilFunctions.isNullOrEmpty(giftUrlInput) ? null : giftUrlInput;

        console.log(this._donatee);

        this.donateeService
            .update(this._donatee!)
            .then(
                (response: IFetchResponse<IDonateeEdit>) => {
                    if (UtilFunctions.isSuccessful(response)) {
                        let isCampaignSpecified = this._campaignId !== '' && this._campaignName !== '';
                        let route = isCampaignSpecified ? 'donateesIndex' : 'campaignsIndex';
                        let params = isCampaignSpecified ? { campaignId: this._campaignId, campaignName: this._campaignName } : {};
                        this.router.navigateToRoute(route, params);
                    } else {
                        this._errorMessage = UtilFunctions.getErrorMessage(response);

                    }
                }
            );

        event.preventDefault();
    }
}

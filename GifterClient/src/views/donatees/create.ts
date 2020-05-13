import { autoinject } from "aurelia-framework";
import { Router } from "aurelia-router";
import { DonateeService } from "service/donateeService";
import * as UtilFunctions from "utils/utilFunctions";
import { IStatus } from "domain/IStatus";
import { IActionType } from "domain/IActionType";
import { StatusService } from "service/statusService";
import { ActionTypeService } from "service/actionTypeService";
import { IDonateeCreate } from '../../domain/IDonatee';
import { Optional } from "types/generalTypes";
import { IFetchResponse } from "types/IFetchResponse";

@autoinject
export class DonateesCreate {
    private _donatee?: IDonateeCreate;

    private _firstName = "";
    private _lastName = null;
    private _gender = null;
    private _age = null;
    private _bio = null;
    private _giftName = "";
    private _giftDescription = null;
    private _giftImage = null;
    private _giftUrl = null;
    private _activeFrom = "";
    private _activeTo = "";
    private _actionTypeId = "";
    private _statusId = "";
    // related tables
    private _statuses: IStatus[] = [];
    private _actionTypes: IActionType[] = [];

    private _errorMessage: Optional<string> = null;

    constructor(
        private donateeService: DonateeService,
        private statusService: StatusService,
        private actionTypeService: ActionTypeService,
        private router: Router
    ) {}

    attached() {
        this.getRelatedData();
    }

    onSubmit(event: Event) {
        let newDonatee: IDonateeCreate = {
            firstName: this._firstName,
            lastName: this._lastName,
            gender: this._gender,
            age: this._age,
            bio: this._bio,
            giftName: this._giftName,
            giftDescription: this._giftDescription,
            giftImage: this._giftImage,
            giftUrl: this._giftUrl,
            activeFrom: this._activeFrom,
            activeTo: this._activeTo,
            actionTypeId: this._actionTypeId,
            statusId: this._statusId
        };
        console.log(newDonatee);
        this.createDonatee(newDonatee);

        event.preventDefault();
    }

    // From other tables that are connected to this one via foreign keys
    private getRelatedData() {
        // this.actionTypeService.getActionTypes().then(
        //     data => this._actionTypes = data
        // );
        // this.statusService.getStatuses().then(
        //     data => this._statuses = data
        // ); TODO
    }

    private createDonatee(newDonatee: IDonateeCreate) {
        this.donateeService
            .create(newDonatee)
            .then((response: IFetchResponse<IDonateeCreate>) => {
                if (UtilFunctions.isSuccessful(response)) {
                    this.router.navigateToRoute("donateesIndex", {});
                } else {
                    this._errorMessage = UtilFunctions.getErrorMessage(response);

                }
            });
    }
}

import { autoinject } from 'aurelia-framework';
import { IActionType } from 'domain/IActionType';
import { ActionTypeService } from 'service/actionTypeService';

@autoinject
export class ActionTypesIndex {
    private _actionTypes: IActionType[] = [];

    constructor(private actionTypeService: ActionTypeService) {

    }

    attached() {
        this.actionTypeService.getActionTypes().then(
            data => this._actionTypes = data
        );
    }
}

import { autoinject } from 'aurelia-framework';
import { ActionTypeService } from '../../service/actiontype-service';
import { IActionType } from '../../domain/IActionType';

@autoinject
export class ActionTypesIndex {
    private _actionTypes: IActionType[] = [];

    constructor(private ActionTypeService: ActionTypeService) {

    }

    attached() {
        this.ActionTypeService.getActionTypes().then(
            data => this._actionTypes = data
        );
    }
}

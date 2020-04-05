import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IActionType } from 'domain/IActionType';
import { ActionTypeService } from 'service/actionTypeService';

@autoinject
export class ActionTypeDetails {
    private _actionTypes: IActionType[] = [];
    private _actionType: Optional<IActionType> = null;

    constructor(private actionTypeService: ActionTypeService) {

    }

    attached() {

    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(params.id && typeof(params.id) === 'string') {
            this.actionTypeService.getActionType(params.id).then(
                data => this._actionType = data
            )
        }
    }
}

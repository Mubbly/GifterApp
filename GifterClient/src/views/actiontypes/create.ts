import { autoinject } from "aurelia-framework";
import { RouteConfig, NavigationInstruction, Router } from "aurelia-router";
import { ActionTypeService } from "service/actionTypeService";
import * as Utils from "utils/utilFunctions";
import { IActionTypeCreate } from "domain/IActionType";
import { Optional } from "types/generalTypes";
import { AppState } from "state/appState";
import { IFetchResponse } from "types/IFetchResponse";

@autoinject
export class ActionTypesCreate {
    private readonly ACTION_TYPES_ROUTE = "actiontypesIndex";
    private readonly ERROR_REQUIRED_FIELDS = "Please fill in required fields!";
    private _actionType?: IActionTypeCreate;
    private _actionTypeValue: string = "";
    private _comment: Optional<string> = "";
    private _errorMessage: Optional<string> = null;
    private _isAdmin: boolean = false;
    private _jwt: boolean = false;

    constructor(
        private actionTypeService: ActionTypeService,
        private router: Router,
        private appState: AppState,
    ) {}

    attached() {}

    activate() {
        const isLoggedIn = this.appState.jwt;
        if(!isLoggedIn) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        }
        this._isAdmin = true; // TODO: Create logic for it
    }

    onSubmit(event: Event) {
        if(Utils.isEmpty(this._actionTypeValue)) {
            this._errorMessage = this.ERROR_REQUIRED_FIELDS;
            return;
        }
        if(Utils.isEmpty(this._comment)) {
            this._comment = null;
        }

        let newActionType: IActionTypeCreate = {
            actionTypeValue: this._actionTypeValue,
            comment: this._comment
        };

        this.createActionType(newActionType);
        event.preventDefault();
    }

    private createActionType(newActionType: IActionTypeCreate) {
        this.actionTypeService
        .create(newActionType)
        .then((response: IFetchResponse<IActionTypeCreate>) => {
            if (!Utils.isSuccessful(response)) {
                this._errorMessage = Utils.getErrorMessage(response);
            } else {
                this.router.navigateToRoute(this.ACTION_TYPES_ROUTE, {});
            }
        });
    }
}

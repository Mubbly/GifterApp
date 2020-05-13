import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IActionTypeEdit } from 'domain/IActionType';
import * as Utils from 'utils/utilFunctions';
import { Optional, GifterInterface, HTML5DateString } from 'types/generalTypes';
import { ActionTypeService } from 'service/actionTypeService';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';

@autoinject
export class ActionTypesEdit {
    private readonly ACTION_TYPES_ROUTE = 'actiontypesIndex';
    private readonly ERROR_REQUIRED_FIELDS = "Please fill in required fields!";

    private _actionType?: IActionTypeEdit;

    private _errorMessage: Optional<string> = null;
    private _isAdmin: boolean = false;

    constructor(private actionTypeService: ActionTypeService, private router: Router, private appState: AppState) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        const isLoggedIn = this.appState.jwt;
        if(!isLoggedIn) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getActionType(params.id);
        }
    }

    onSubmit(event: Event) {
        this.getNewValuesFromInputs();
        this.updateActionType();
        
        event.preventDefault();
    }

    /** Reassigns _actionType props */
    private getNewValuesFromInputs() {
        let actionTypeValueInput = <string>this._actionType!.actionTypeValue;

        if(Utils.isEmpty(actionTypeValueInput)) {
            this._errorMessage = this.ERROR_REQUIRED_FIELDS;
            return;
        }
        let commentInput = <Optional<string>>this._actionType!.comment;
        this._actionType!.comment = Utils.isEmpty(commentInput) ? null : commentInput;
    }

    private updateActionType(): void {
        this.actionTypeService
            .update(this._actionType!)
            .then(
                (response: IFetchResponse<IActionTypeEdit>) => {
                    if (!Utils.isSuccessful(response)) {
                        this._errorMessage = Utils.getErrorMessage(response);
                    } else {
                        this.router.navigateToRoute(this.ACTION_TYPES_ROUTE, {});
                    }
                }
            );
    }

    private getActionType(id: string): void {
        if (Utils.existsAndIsString(id)) {
            this.actionTypeService
                .get(id)
                .then((response) => {
                    if (!Utils.isSuccessful(response)) {
                        this.handleErrors(response);
                    } else {
                        this.setAsAdmin(true);
                        this._actionType = response.data!;
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
                this.setAsAdmin(false);
                break;
            default:
                this._errorMessage = Utils.getErrorMessage(response);
        }
    }

    private setAsAdmin(isAdmin: boolean) {
        this._isAdmin = isAdmin;
    }
}

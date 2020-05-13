import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { ActionTypeService } from 'service/actionTypeService';
import * as Utils from 'utils/utilFunctions';
import { IActionType } from 'domain/IActionType';
import { Optional, GifterInterface } from 'types/generalTypes';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';

@autoinject
export class ActionTypesDelete {
    private readonly ACTION_TYPES_ROUTE = 'actiontypesIndex';

    private _actionType?: IActionType;
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
        this.deleteActionType(this._actionType!.id);
        event.preventDefault();
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

    private deleteActionType(id: string) {
        this.actionTypeService
        .delete(id)
        .then(
            response => {
                if (!Utils.isSuccessful(response)) {
                    this._errorMessage = Utils.getErrorMessage(response);
                } else {
                    this.router.navigateToRoute(this.ACTION_TYPES_ROUTE, {});
                }
            }
        );
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

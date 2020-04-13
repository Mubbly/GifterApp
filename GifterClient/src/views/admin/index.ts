import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { Optional, GifterInterface } from 'types/generalTypes';
import { AppState } from 'state/appState';
import * as Utils from 'utils/utilFunctions';
import { IFetchResponse } from 'types/IFetchResponse';
import { ActionTypeService } from 'service/actionTypeService';

@autoinject
export class AdminIndex {
    private readonly ERROR_NOT_AN_ADMIN = "You are not an admin";
    private _errorMessage: Optional<string> = null;
    private _isAdmin: boolean = false;

    constructor(
        private actionTypeService: ActionTypeService,
        private router: Router,
        private appState: AppState
    ) {}

    attached() {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            // TODO: GET RID OF THIS DEPENDENCY! Backend should provide a specific method for getting user role info.
            this.getActionTypes();
        }
    }
    
    private getActionTypes(): void {
        this.actionTypeService
        .getActionTypes()
        .then((response) => {
            if (!Utils.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                this.setAsAdmin(true);
            }
        })
        .catch((error) => {
            console.log(error);
        });
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
        this._errorMessage = isAdmin ? null : this.ERROR_NOT_AN_ADMIN; 
    }
}

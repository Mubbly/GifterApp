import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { IStatus } from 'domain/IStatus';
import { StatusService } from 'service/statusService';
import { Optional, GifterInterface } from 'types/generalTypes';
import { AppState } from 'state/appState';
import * as Utils from 'utils/utilFunctions';
import { IFetchResponse } from 'types/IFetchResponse';

@autoinject
export class StatusesIndex {
    private _statuses: IStatus[] = [];
    private _errorMessage: Optional<string> = null;
    private _isAdmin: boolean = false;

    constructor(
        private statusService: StatusService,
        private router: Router,
        private appState: AppState
    ) {}

    attached() {}

    activate() {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getStatuses();
        }
    }
    
    private getStatuses(): void {
        this.statusService
        .getAll()
        .then((response) => {
            if (!Utils.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                this.setAsAdmin(true);
                this._statuses = response.data!;
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
    }
}

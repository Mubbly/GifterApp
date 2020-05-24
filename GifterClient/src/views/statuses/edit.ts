import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IStatusEdit } from 'domain/IStatus';
import * as Utils from 'utils/utilFunctions';
import { Optional, GifterInterface, HTML5DateString } from 'types/generalTypes';
import { StatusService } from 'service/statusService';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';

@autoinject
export class StatusesEdit {
    private readonly STATUSES_ROUTE = 'statusesIndex';
    private readonly ERROR_REQUIRED_FIELDS = "Please fill in required fields!";

    private _status?: IStatusEdit;

    private _errorMessage: Optional<string> = null;
    private _isAdmin: boolean = false;

    constructor(private statusService: StatusService, private router: Router, private appState: AppState) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        const isLoggedIn = this.appState.jwt;
        if(!isLoggedIn) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getStatus(params.id);
        }
    }

    onSubmit(event: Event) {
        this.getNewValuesFromInputs();
        this.updateStatus();
        
        event.preventDefault();
    }

    /** Reassigns _status props */
    private getNewValuesFromInputs() {
        let statusValueInput = <string>this._status!.statusValue;

        if(Utils.isNullOrEmpty(statusValueInput)) {
            this._errorMessage = this.ERROR_REQUIRED_FIELDS;
            return;
        }
        let commentInput = <Optional<string>>this._status!.comment;
        this._status!.comment = Utils.isNullOrEmpty(commentInput) ? null : commentInput;
    }

    private updateStatus(): void {
        this.statusService
            .update(this._status!)
            .then(
                (response: IFetchResponse<IStatusEdit>) => {
                    if (!Utils.isSuccessful(response)) {
                        this._errorMessage = Utils.getErrorMessage(response);
                    } else {
                        this.router.navigateToRoute(this.STATUSES_ROUTE, {});
                    }
                }
            );
    }

    private getStatus(id: string): void {
        if (Utils.existsAndIsString(id)) {
            this.statusService
                .get(id)
                .then(response => {
                    if (!Utils.isSuccessful(response)) {
                        this.handleErrors(response);
                    } else {
                        this.setAsAdmin(true);
                        this._status = response.data!;
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

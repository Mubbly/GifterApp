import { autoinject } from "aurelia-framework";
import { RouteConfig, NavigationInstruction, Router } from "aurelia-router";
import { Optional, GifterInterface } from "types/generalTypes";
import { IStatus } from "domain/IStatus";
import { StatusService } from "service/statusService";
import * as Utils from "utils/utilFunctions";
import { IFetchResponse } from "types/IFetchResponse";
import { AppState } from "state/appState";

@autoinject
export class StatusDetails {
    private _status: Optional<IStatus> = null;
    private _errorMessage: Optional<string> = null;
    private _isAdmin: boolean = false;

    constructor(
        private statusService: StatusService,
        private router: Router,
        private appState: AppState
    ) {}

    attached() {}

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        const isLoggedIn = this.appState.jwt;
        if(!isLoggedIn) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getStatus(params.id);
        }
    }

    private getStatus(id: string): void {
        if (Utils.existsAndIsString(id)) {
            this.statusService.get(id).then((response) => {
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

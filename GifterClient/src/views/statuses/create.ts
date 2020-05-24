import { autoinject } from "aurelia-framework";
import { Router } from "aurelia-router";
import { StatusService } from "service/statusService";
import * as Utils from "utils/utilFunctions";
import { IStatusCreate } from "domain/IStatus";
import { Optional } from "types/generalTypes";
import { AppState } from "state/appState";

@autoinject
export class StatusesCreate {
    private readonly STATUSES_ROUTE = "statusesIndex";
    private readonly ERROR_REQUIRED_FIELDS = "Please fill in required fields!";
    private _status?: IStatusCreate;
    private _statusValue: string = "";
    private _comment: Optional<string> = "";
    private _errorMessage: Optional<string> = null;
    private _isAdmin: boolean = false;
    private _jwt: boolean = false;

    constructor(
        private statusService: StatusService,
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
        if(Utils.isNullOrEmpty(this._statusValue)) {
            this._errorMessage = this.ERROR_REQUIRED_FIELDS;
            return;
        }
        if(Utils.isNullOrEmpty(this._comment)) {
            this._comment = null;
        }

        let newStatus: IStatusCreate = {
            statusValue: this._statusValue,
            comment: this._comment
        };

        this.createStatus(newStatus);
        event.preventDefault();
    }

    private createStatus(newStatus: IStatusCreate) {
        this.statusService
        .create(newStatus)
        .then(response => {
            if (!Utils.isSuccessful(response)) {
                this._errorMessage = Utils.getErrorMessage(response);
            } else {
                this.router.navigateToRoute(this.STATUSES_ROUTE, {});
            }
        });
    }
}

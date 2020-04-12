import { Router } from "aurelia-router";
import { autoinject } from "aurelia-framework";
import { AccountService } from "service/accountService";
import { Optional } from "types/generalTypes";
import { AppState } from "state/appState";
import { isSuccessful } from "utils/utilFunctions";
import { App } from "app";

@autoinject
export class AccountLogin {
    private readonly ERROR_MSG_CANT_FIND_USER = "Cannot find such user. Please overview your credentials and try again.";
    
    private _email = "";
    private _password = "";
    private _errorMessage: Optional<string> = null;

    constructor(
        private app: App,
        private accountService: AccountService,
        private appState: AppState,
        private router: Router
    ) {}

    onSubmit(event: Event) {
        event.preventDefault();
        this.logIn(this._email, this._password);
    }

    logIn(email: string, password: string) {
        this.accountService
        .login(email, password)
        .then((response) => {
            if (isSuccessful(response)) {
                this.appState.jwt = response.data!.token;
                console.log(this.appState.jwt);
                this.router!.navigateToRoute(this.app.HOME_ROUTE);
            } else {
                //let statusCode = response.status.toString();
                this._errorMessage = this.ERROR_MSG_CANT_FIND_USER;
            }
        });
    }
}

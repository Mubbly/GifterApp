import { Router } from "aurelia-router";
import { autoinject } from "aurelia-framework";
import { AccountService } from "service/accountService";
import { Optional } from "types/generalTypes";
import { AppState } from "state/appState";
import { isSuccessful } from "utils/utilFunctions";
import { App } from "app";

@autoinject
export class AccountLogin {
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
        console.log(this._email, this._password);
        event.preventDefault();

        this.accountService
            .login(this._email, this._password)
            .then((response) => {
                if (isSuccessful(response)) {
                    console.log("If: ", response);
                    this.appState.jwt = response.data!.token;
                    this.router!.navigateToRoute(this.app.HOME_ROUTE);
                } else {
                    let statusCode = response.status.toString();
                    this._errorMessage = `${statusCode} ${response.errorMessage}`;
                }
            });
    }
}

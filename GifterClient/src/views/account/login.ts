import { Router } from "aurelia-router";
import { autoinject } from "aurelia-framework";
import { AccountService } from "service/base/accountService";
import { Optional } from "types/generalTypes";
import { AppState } from "state/appState";
import { isSuccessful } from "utils/utilFunctions";
import { App } from "app";
import * as UtilFunctions from 'utils/utilFunctions';
import { ILoginResponse } from "types/ILoginResponse";
import { ICurrentUser } from "domain/IAppUser";

@autoinject
export class AccountLogin {
    private readonly ERROR_MSG_CANT_FIND_USER = "Cannot find such user. Please overview your credentials and try again.";
    private readonly ERROR_SERVER = "Server error. Please try again later.";

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
            console.log(response);
            if (isSuccessful(response)) {
                let responseData: ILoginResponse = response.data!;
                this.appState.jwt = responseData.token;
                this.appState.userId = responseData.id;
                this.appState.userFullName = `${responseData.firstName} ${responseData.lastName}`;

                this.router!.navigateToRoute(this.app.HOME_ROUTE);
            } else {
                localStorage.clear();
                if(!response.status || response.status === UtilFunctions.STATUS_CODE_SERVER_ERROR) {
                    this._errorMessage = this.ERROR_SERVER;
                } else {
                    this._errorMessage = this.ERROR_MSG_CANT_FIND_USER;
                }
            }
        });
    }
}

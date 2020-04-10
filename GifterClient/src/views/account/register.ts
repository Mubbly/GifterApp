import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { App } from 'app';
import { AccountService } from 'service/accountService';
import { AppState } from 'state/appState';
import { isSuccessful } from 'utils/utilFunctions';
import { Optional } from 'types/generalTypes';
import { AccountLogin } from './login';

@autoinject
export class AccountRegister {    
    private readonly ERROR_MSG_PASSWORDS_DONT_MATCH = "Password and confirmation password don't match";
    private readonly ERROR_MSG_CANT_REGISTER = "Cannot register user";
    private readonly REGISTRATION_SUCCESSFUL = "Registration was successful. We've sent a message to your e-mail, please confirm it to log in!"
    private readonly LOGIN_ROUTE = "accountLogin"

    private _email = "";
    private _firstName = "";
    private _lastName = "";
    private _password = "";
    private _passwordConfirm = "";
    private _errorMessage: Optional<string> = null;
    private _successMessage: Optional<string> = null;

    constructor(
        private accountService: AccountService,
        private router: Router
    ) {}

    onSubmit(event: Event) {
        console.log(this._email, this._firstName, this._lastName, this._password, this._passwordConfirm);
        event.preventDefault();

        if(this._password !== this._passwordConfirm) {
            this._errorMessage = this.ERROR_MSG_PASSWORDS_DONT_MATCH;
            return;
        }

        this.accountService
            .register(this._email, this._firstName, this._lastName, this._password)
            .then((response) => {
                if(isSuccessful(response)) {
                    console.log("Account registered");
                    this._successMessage = this.REGISTRATION_SUCCESSFUL;
                } else {
                    console.log("Account NOT registered");
                    let statusCode = response.status.toString();
                    this._errorMessage = `${statusCode} ${response.errorMessage} - ${this.ERROR_MSG_CANT_REGISTER}`;
                }
            });
        // this.accountService
        //     .login(this._email, this._password)
        //     .then((response) => {
        //         if (isSuccessful(response)) {
        //             console.log("If: ", response);
        //             this.appState.jwt = response.data!.token;
        //             this.router!.navigateToRoute(this.app.HOME_ROUTE);
        //         } else {
        //             let statusCode = response.status.toString();
        //             this._errorMessage = `${statusCode} ${response.errorMessage}`;
        //             alert(this.ERROR_MSG_CANT_FIND_USER);
        //         }
        //     });
    }
}

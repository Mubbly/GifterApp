import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { App } from 'app';
import { AccountService } from 'service/accountService';
import { AppState } from 'state/appState';
import { isSuccessful } from 'utils/utilFunctions';
import { Optional } from 'types/generalTypes';
import { AccountLogin } from './login';
import * as UtilFunctions from 'utils/utilFunctions';

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
        event.preventDefault();

        // Check password inputs match
        if(this._password !== this._passwordConfirm) {
            this._errorMessage = this.ERROR_MSG_PASSWORDS_DONT_MATCH;
            return;
        }

        this.accountService
            .register(this._email, this._firstName, this._lastName, this._password)
            .then((response) => {
                if(!isSuccessful(response)) {
                    //let statusCode = response.status.toString();
                    this._errorMessage = UtilFunctions.getErrorMessage(response, this.ERROR_MSG_CANT_REGISTER);
                } else {
                    this._successMessage = this.REGISTRATION_SUCCESSFUL;
                }
            });
    }
}

import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { AccountService } from 'service/base/accountService';
import { isSuccessful } from 'utils/utilFunctions';
import { Optional } from 'types/generalTypes';
import * as UtilFunctions from 'utils/utilFunctions';
import { AccountLogin } from './login';

@autoinject
export class AccountRegister {    
    private readonly ERROR_MSG_PASSWORDS_DONT_MATCH = "Password and confirmation password don't match";
    private readonly ERROR_MSG_CANT_REGISTER = "Cannot register user";
    private readonly REGISTRATION_SUCCESSFUL = "Registration was successful! Please log in."
    private readonly LOGIN_ROUTE = "accountLogin"

    private _email = "";
    private _firstName = "";
    private _lastName = "";
    private _password = "";
    private _passwordConfirm = "";
    private _errorMessage: Optional<string> = null;
    private _successMessage: Optional<string> = null;
    private _jwt: Optional<string> = null;

    constructor(
        private accountService: AccountService,
        private loginAction: AccountLogin,
        private router: Router
    ) {}

    onSubmit(event: Event) {
        event.preventDefault();

        // Check password inputs match
        if(this._password !== this._passwordConfirm) {
            this._errorMessage = this.ERROR_MSG_PASSWORDS_DONT_MATCH;
            return;
        }
        this.register(this._email, this._firstName, this._lastName, this._password);
    }

    private register(email: string, firstName: string, lastName: string, password: string) {
        this.accountService
            .register(email, firstName, lastName, password)
            .then((response) => {
                if(!isSuccessful(response)) {
                    this._errorMessage = UtilFunctions.getErrorMessage(response, this.ERROR_MSG_CANT_REGISTER);
                } else {
                    // Automatic login after successful registration
                    this._jwt = response.data ? response.data.token : null;
                    if(this._jwt != null) {
                        this.loginAction.logIn(this._email, this._password);
                    }
                }
            });
    }
}

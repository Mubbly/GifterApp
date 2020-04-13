import { autoinject } from "aurelia-framework";
import { RouteConfig, NavigationInstruction, Router } from "aurelia-router";
import { InvitedUserService } from "service/invitedUserService";
import * as Utils from "utils/utilFunctions";
import { IInvitedUserCreate } from "domain/IInvitedUserCreate";
import { Optional } from "types/generalTypes";
import { AppState } from "state/appState";

@autoinject
export class InvitedUsersCreate {
    private readonly INVITED_USERS_ROUTE = "invitedusersIndex";
    private readonly ERROR_REQUIRED_FIELDS = "Please fill in required fields!";
    
    private _invitedUser?: IInvitedUserCreate;
    private _email: string = "";
    private _phoneNumber: Optional<string> = null;
    private _message: Optional<string> = null;

    private _errorMessage: Optional<string> = null;
    private _jwt: boolean = false;

    constructor(
        private invitedUserService: InvitedUserService,
        private router: Router,
        private appState: AppState,
    ) {}

    attached() {}

    activate() {
        const isLoggedIn = this.appState.jwt;
        if(!isLoggedIn) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        }
    }

    // TODO: Email and phonenumber validations!
    onSubmit(event: Event) {
        event.preventDefault();
        // Required fields
        if(Utils.isEmpty(this._email)) {
            this._errorMessage = this.ERROR_REQUIRED_FIELDS;
            return;
        }
        let newInvitedUser: IInvitedUserCreate = {
            email: this._email,
            phoneNumber: this._phoneNumber,
            message: this._message
        };
        this.createInvitedUser(newInvitedUser);
    }

    private createInvitedUser(newInvitedUser: IInvitedUserCreate) {
        this.invitedUserService
        .createInvitedUser(newInvitedUser)
        .then((response) => {
            if (!Utils.isSuccessful(response)) {
                this._errorMessage = Utils.getErrorMessage(response);
            } else {
                this.router.navigateToRoute(this.INVITED_USERS_ROUTE, {});
            }
        });
    }
}

import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IInvitedUserEdit } from 'domain/IInvitedUserEdit';
import * as Utils from 'utils/utilFunctions';
import { Optional, GifterInterface, HTML5DateString } from 'types/generalTypes';
import { InvitedUserService } from 'service/invitedUserService';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';

@autoinject
export class InvitedUsersEdit {
    private readonly INVITED_USERS_ROUTE = 'invitedusersIndex';
    private readonly ERROR_REQUIRED_FIELDS = "Please fill in required fields!";

    private _invitedUser?: IInvitedUserEdit;
    private _errorMessage: Optional<string> = null;

    constructor(private invitedUserService: InvitedUserService, private router: Router, private appState: AppState) {
    }

    attached() {
    }

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        const isLoggedIn = this.appState.jwt;
        if(!isLoggedIn) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getInvitedUser(params.id);
        }
    }

    onSubmit(event: Event) {
        this.getNewValuesFromInputs();
        this.updateInvitedUser();
        
        event.preventDefault();
    }

    // TODO: Email and phonenumber validations!
    /** Reassigns _invitedUser props */
    private getNewValuesFromInputs() {
        console.log(this._invitedUser);
        // Required fields
        let emailInput = <string>this._invitedUser!.email;
        if(Utils.isEmpty(emailInput)) {
            this._errorMessage = this.ERROR_REQUIRED_FIELDS;
            return;
        }
        this._invitedUser!.email = emailInput;
        // Optional fields
        let phoneNumberInput = <Optional<string>>this._invitedUser!.phoneNumber;
        this._invitedUser!.phoneNumber = Utils.isEmpty(phoneNumberInput) ? null : phoneNumberInput;
        let messageInput = <Optional<string>>this._invitedUser!.message;
        this._invitedUser!.message = Utils.isEmpty(messageInput) ? null : messageInput;
    }

    private updateInvitedUser(): void {
        this.invitedUserService
            .updateInvitedUser(this._invitedUser!)
            .then(
                response => {
                    if (!Utils.isSuccessful(response)) {
                        this._errorMessage = Utils.getErrorMessage(response);
                    } else {
                        this.router.navigateToRoute(this.INVITED_USERS_ROUTE, {});
                    }
                }
            );
    }

    private getInvitedUser(id: string): void {
        if (Utils.existsAndIsString(id)) {
            this.invitedUserService
                .getInvitedUser(id)
                .then((response) => {
                    if (!Utils.isSuccessful(response)) {
                        this.handleErrors(response);
                    } else {
                        this._invitedUser = response.data!;
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
            default:
                this._errorMessage = Utils.getErrorMessage(response);
        }
    }
}

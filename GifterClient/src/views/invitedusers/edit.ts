import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { IInvitedUserEdit } from 'domain/IInvitedUser';
import * as Utils from 'utils/utilFunctions';
import { Optional, GifterInterface, HTML5DateString } from 'types/generalTypes';
import { InvitedUserService } from 'service/invitedUserService';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';

@autoinject
export class InvitedUsersEdit {
    private readonly INVITE_MAIL_DEFAULT_SUBJECT = 'Hey friend, Im inviting you to join GifterApp!';
    private readonly INVITE_MAIL_DEFAULT_BODY = "Hi! \n\n There is this awesome app called GifterApp that I'm using. \n Please check out their website and join as well! Here is the url: http://localhost:8080/"; // TODO: Change to actual url
    private readonly INVITE_MAIL_DEFAULT_BODY_SIGNATURE = `\n\n Best wishes, ${this.appState.userFullName}`;
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
        if(Utils.isNullOrEmpty(emailInput)) {
            this._errorMessage = this.ERROR_REQUIRED_FIELDS;
            return;
        }
        this._invitedUser!.email = emailInput;
        // Optional fields
        let phoneNumberInput = <Optional<string>>this._invitedUser!.phoneNumber;
        this._invitedUser!.phoneNumber = Utils.isNullOrEmpty(phoneNumberInput) ? null : phoneNumberInput;
        let messageInput = <Optional<string>>this._invitedUser!.message;
        this._invitedUser!.message = Utils.isNullOrEmpty(messageInput) ? null : messageInput;
    }

    private updateInvitedUser(): void {
        this.invitedUserService
            .update(this._invitedUser!)
            .then(
                (response: IFetchResponse<IInvitedUserEdit>) => {
                    if (!Utils.isSuccessful(response)) {
                        this._errorMessage = Utils.getErrorMessage(response);
                    } else {
                        // Open e-mail client with necessary info filled in
                        this.sendEmail(this._invitedUser!);
                        // Redirect to invited friends list
                        this.router.navigateToRoute(this.INVITED_USERS_ROUTE, {});
                    }
                }
            );
    }

    private getInvitedUser(id: string): void {
        if (Utils.existsAndIsString(id)) {
            this.invitedUserService
                .getPersonal(id)
                .then(response => {
                    if (!Utils.isSuccessful(response)) {
                        this.handleErrors(response);
                    } else {
                        this._invitedUser = response.data!;
                    }
                });
        }
    }

        /** Opens e-mail client with necessary info (email address, subject, body) filled in */
        private sendEmail(newInvitedUser: IInvitedUserEdit): void {
            let invitedUserEmail = newInvitedUser.email;
    
            if(this.isValidEmail(invitedUserEmail)) {
                // Construct e-mail
                const emailAddress = `mailto:${invitedUserEmail}`;
                const emailSubject = `?subject=${encodeURIComponent(this.INVITE_MAIL_DEFAULT_SUBJECT)};`
                const bodyMessage = !Utils.isNullOrEmpty(newInvitedUser.message) ? (newInvitedUser.message + this.INVITE_MAIL_DEFAULT_BODY_SIGNATURE) : (this.INVITE_MAIL_DEFAULT_BODY + this.INVITE_MAIL_DEFAULT_BODY_SIGNATURE);
                const emailBody = `&body=${encodeURIComponent(bodyMessage)}`;
    
                let emailLink = `${emailAddress}${emailSubject}${emailBody}`;
    
                // Open e-mail client
                window.location.href = emailLink;
            }
        }
    
        /** Replace this silly validation with actual one */
        private isValidEmail(email: string): boolean {
            return Utils.existsAndIsString(email) && email.includes('@') && email.includes('.');
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

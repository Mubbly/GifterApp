import { autoinject } from "aurelia-framework";
import { RouteConfig, NavigationInstruction, Router } from "aurelia-router";
import { InvitedUserService } from "service/invitedUserService";
import * as Utils from "utils/utilFunctions";
import { IInvitedUserCreate } from "domain/IInvitedUser";
import { Optional } from "types/generalTypes";
import { AppState } from "state/appState";
import { IFetchResponse } from "types/IFetchResponse";

@autoinject
export class InvitedUsersCreate {
    private readonly INVITE_MAIL_DEFAULT_SUBJECT = 'Hey friend, Im inviting you to join GifterApp!';
    private readonly INVITE_MAIL_DEFAULT_BODY = "Hi! \n\n There is this awesome app called GifterApp that I'm using. \n Please check out their website and join as well! Here is the url: http://localhost:8080/"; // TODO: Change to actual url
    private readonly INVITE_MAIL_DEFAULT_BODY_SIGNATURE = `\n\n Best wishes, ${this.appState.userFullName}`;
    private readonly INVITED_USERS_ROUTE = "invitedusersIndex";
    private readonly ERROR_REQUIRED_FIELDS = "Please fill in required fields!";
    private readonly ERROR_WRONG_EMAIL_FORMAT = 'Please use the correct e-mail format. Example: example@email.com'
    
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
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        }
    }

    // TODO: Email and phonenumber validations
    onSubmit(event: Event) {
        event.preventDefault();
        // Required fields
        if(Utils.isNullOrEmpty(this._email)) {
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
        .create(newInvitedUser)
        .then((response) => {
            if (!Utils.isSuccessful(response)) {
                this._errorMessage = Utils.getErrorMessage(response);
            } else {
                if(this.isValidEmail(newInvitedUser.email)) {
                    // Open e-mail client with necessary info filled in
                    this.sendEmail(newInvitedUser);
                    // Redirect to invited friends list
                    this.router.navigateToRoute(this.INVITED_USERS_ROUTE, {});
                } else {
                    this._errorMessage = this.ERROR_WRONG_EMAIL_FORMAT;
                }
            }
        });
    }

    /** Opens e-mail client with necessary info (email address, subject, body) filled in */
    private sendEmail(newInvitedUser: IInvitedUserCreate): void {
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
}

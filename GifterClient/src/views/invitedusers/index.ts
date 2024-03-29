import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-framework';
import { IInvitedUser } from 'domain/IInvitedUser';
import { InvitedUserService } from 'service/invitedUserService';
import { Optional, GifterInterface } from 'types/generalTypes';
import { AppState } from 'state/appState';
import * as Utils from 'utils/utilFunctions';
import { IFetchResponse } from 'types/IFetchResponse';

@autoinject
export class InvitedUsersIndex {
    private _invitedUsers: IInvitedUser[] = [];
    private _errorMessage: Optional<string> = null;

    constructor(
        private invitedUserService: InvitedUserService,
        private router: Router,
        private appState: AppState
    ) {}

    attached() {}

    activate() {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this.getInvitedUsers();
        }
    }
    
    private getInvitedUsers(): void {
        this.invitedUserService
        .getAllPersonal()
        .then((response) => {
            if (!Utils.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                this._invitedUsers = response.data!;
                
                this._invitedUsers.forEach(user => {
                    user.dateInvited = Utils.formatAsHtml5Date(user.dateInvited);
                });
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    /**
     * Set error message or route to login/home page
     */
    private handleErrors(response: IFetchResponse<GifterInterface | GifterInterface[]>) {
        switch(response.status) {
            case Utils.STATUS_CODE_UNAUTHORIZED:
                this.router.navigateToRoute(Utils.LOGIN_ROUTE);
                break;
            case Utils.STATUS_CODE_NOT_FOUND:
                this._errorMessage = "No invited friends";
                break;
            default:
                this._errorMessage = Utils.getErrorMessage(response);
        }
    }
}

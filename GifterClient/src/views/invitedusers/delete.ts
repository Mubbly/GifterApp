import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { InvitedUserService } from 'service/invitedUserService';
import * as Utils from 'utils/utilFunctions';
import { IInvitedUser } from 'domain/IInvitedUser';
import { Optional, GifterInterface } from 'types/generalTypes';
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';

@autoinject
export class InvitedUsersDelete {
    private readonly INVITED_USERS_ROUTE = 'invitedusersIndex';

    private _invitedUser?: IInvitedUser;
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
        this.deleteInvitedUser(this._invitedUser!.id);
        event.preventDefault();
    }

    private getInvitedUser(id: string): void {
        if (Utils.existsAndIsString(id)) {
            this.invitedUserService
                .get(id)
                .then((response) => {
                    if (!Utils.isSuccessful(response)) {
                        this.handleErrors(response);
                    } else {
                        this._invitedUser = response.data!;
                    }
                });
        }
    }

    private deleteInvitedUser(id: string) {
        this.invitedUserService
        .delete(id)
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

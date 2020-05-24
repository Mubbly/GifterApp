import { Router } from 'aurelia-router';
import { autoinject } from "aurelia-framework";
import { IFriendship } from "domain/IFriendship";
import { FriendshipService } from "service/friendshipService";
import * as UtilFunctions from "utils/utilFunctions";
import { Optional, GifterInterface } from "types/generalTypes";
import { IFetchResponse } from "types/IFetchResponse";
import * as Utils from 'utils/utilFunctions';
import { AppState } from 'state/appState';
import { ProfileService } from 'service/profileService';

@autoinject
export class FriendshipsIndex {
    private readonly ERROR_MESSAGE_NO_FRIENDS_ADDED = 'You have not added any friends. ¯\\_(ツ)_/¯';
    private _confirmedFriendships: IFriendship[] = [];
    private _errorMessage: Optional<string> = null;

    constructor(private friendshipService: FriendshipService, 
        private profileService: ProfileService,
        private router: Router, 
        private appState: AppState) {}

    attached() {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        }
        this.getPersonalConfirmedFriendships();
    }

    private getPersonalConfirmedFriendships(): Promise<void> {
        return this.friendshipService
        .getAllPersonal()
        .then((response) => {
            if (!UtilFunctions.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                this._confirmedFriendships = response.data!;
                if(this._confirmedFriendships.length <= 0) {
                    this._errorMessage = this.ERROR_MESSAGE_NO_FRIENDS_ADDED;
                }
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
            default:
                this._errorMessage = Utils.getErrorMessage(response);
        }
    }
}

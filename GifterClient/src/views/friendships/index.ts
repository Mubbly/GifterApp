import { Router } from 'aurelia-router';
import { autoinject } from "aurelia-framework";
import { IFriendship, IFriendshipResponse } from "domain/IFriendship";
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
    private _confirmedFriendships: IFriendshipResponse[] = [];

    private _errorMessage: Optional<string> = null;

    private _showLoader: boolean = false;

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

    onUnfriend(event: Event, friendId: string) {
        event.preventDefault();
        this.deleteFriendship(friendId);
    }

    /** Delete existing confirmed friendship */
    private deleteFriendship(friendId: string): void {
        this.friendshipService
        .delete(friendId)
        .then((response) => {
            if (!Utils.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                Utils.refreshPage();
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    private getPersonalConfirmedFriendships(): Promise<void> {
        this._showLoader = true;
        return this.friendshipService
        .getAllConfirmed()
        .then((response) => {
            this._showLoader = false;
            if (!UtilFunctions.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                if(!response.data || response.data.length <= 0) {
                    this._errorMessage = this.ERROR_MESSAGE_NO_FRIENDS_ADDED;
                    return;
                }
                this._confirmedFriendships = response.data!;
                this._confirmedFriendships.forEach(friend => {
                    friend.lastActive = Utils.formatAsHtml5Date(friend.lastActive);
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
            default:
                this._errorMessage = Utils.getErrorMessage(response);
        }
    }
}

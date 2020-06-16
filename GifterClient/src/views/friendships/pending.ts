import { Router } from 'aurelia-router';
import { autoinject } from "aurelia-framework";
import { IFriendship, IFriendshipEdit } from "domain/IFriendship";
import { FriendshipService } from "service/friendshipService";
import * as UtilFunctions from "utils/utilFunctions";
import { Optional, GifterInterface } from "types/generalTypes";
import { IFetchResponse } from "types/IFetchResponse";
import * as Utils from 'utils/utilFunctions';
import { AppState } from 'state/appState';
import { STATUS_CODE_NOT_FOUND } from '../../utils/utilFunctions';
import { IFriendshipResponse } from '../../domain/IFriendship';
import { PermissionDetails } from '../permissions/details';

@autoinject
export class FriendshipsIndex {
    private readonly MESSAGE_FRIENDSHIP_CONFIRMED = "Friendship confirmed";
    private readonly MESSAGE_FRIENDSHIP_DELETED = "Friendship deleted";

    // private _pendingFriendships: Optional<IFriendshipResponse[]> = null;
    private _pendingSentFriendships: Optional<IFriendshipResponse[]> = null;
    private _pendingReceivedFriendships: Optional<IFriendshipResponse[]> = null;

    private _successMessage: Optional<string> = null;
    private _errorMessage: Optional<string> = null;

    constructor(private friendshipService: FriendshipService, 
        private router: Router, private appState: AppState) {}

    attached() {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        }
        this.getPersonalSentFriendRequests();
        this.getPersonalReceivedFriendRequests();
    }

    onAcceptFriend(event: Event, friendId: string) {
        event.preventDefault();
        this.confirmPendingFriendship(friendId);
    }

    onDeclineFriend(event: Event, friendId: string) {
        event.preventDefault();
        this.deleteFriendship(friendId);
    }

    private getPersonalSentFriendRequests(): void {
        this.friendshipService
        .getAllSentPendingFriendships()
        .then((response) => {
            if (!UtilFunctions.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                if(response.data && (response.data).length > 0) {
                    this._pendingSentFriendships = response.data;

                    this._pendingSentFriendships.forEach(friend => {
                        friend.lastActive = Utils.formatAsHtml5Date(friend.lastActive);
                    });
                }
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }
    
    private getPersonalReceivedFriendRequests(): void {
        this.friendshipService
        .getAllReceivedPendingFriendships()
        .then((response) => {
            if (!UtilFunctions.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                if(response.data && (response.data).length > 0) {
                    this._pendingReceivedFriendships = response.data;

                    this._pendingReceivedFriendships.forEach(friend => {
                        friend.lastActive = Utils.formatAsHtml5Date(friend.lastActive);
                    });
                }
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    private confirmPendingFriendship(friendId: string): Promise<void> {
        return this.friendshipService
            .getPending(friendId)
            .then((response) => {
                if (!UtilFunctions.isSuccessful(response)) {
                    this.handleErrors(response);
                } else {
                    if(response.data) {
                        let friendship: IFriendshipEdit = response.data;
                        this.updateFriendshipStatus(friendship);
                    }
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }

    /** Update friendship to confirmed status in case it's currently pending */
    private updateFriendshipStatus(friendship: IFriendshipEdit): Promise<void> {
        return this.friendshipService
            .updateToConfirmedStatus(friendship)
            .then((response) => {
                if (!UtilFunctions.isSuccessful(response)) {
                    this.handleErrors(response);
                } else {
                    // this._successMessage = this.MESSAGE_FRIENDSHIP_CONFIRMED;
                    Utils.refreshPage();
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }

    /** Delete existing confirmed friendship or pending request */
    private deleteFriendship(friendId: string): void {
        this.friendshipService
        .delete(friendId)
        .then((response) => {
            if (!UtilFunctions.isSuccessful(response)) {
                this.handleErrors(response);
            } else {
                // this._successMessage = this.MESSAGE_FRIENDSHIP_DELETED;
                Utils.refreshPage();
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    /**
     * Set error message or route to login/home page
     */
    private handleErrors(response: IFetchResponse<GifterInterface | GifterInterface[] | IFriendshipEdit>) {
        switch(response.status) {
            case Utils.STATUS_CODE_UNAUTHORIZED:
                this.router.navigateToRoute(Utils.LOGIN_ROUTE);
                break;
            default:
                this._errorMessage = Utils.getErrorMessage(response);
                this._successMessage = null;
        }
    }
}

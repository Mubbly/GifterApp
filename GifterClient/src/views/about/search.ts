import { Router } from 'aurelia-router';
import { autoinject } from "aurelia-framework";
import * as Utils from "utils/utilFunctions";
import { Optional, GifterInterface } from "types/generalTypes";
import { AppState } from 'state/appState';
import { IFetchResponse } from 'types/IFetchResponse';
import { AppUserService } from 'service/base/appUserService';
import { IAppUser } from 'domain/IAppUser';
import { FriendshipService } from 'service/friendshipService';

@autoinject
export class UsersIndex {
    //private readonly SEARCH_FOR_ALL_KEYWORD = "*";
    private readonly MESSAGE_FRIEND_REQUEST_SENT = 'Friend request sent';
    private _users: IAppUser[] = [];
    private _searchInput: string = '';
    private _isFriend: boolean = false;
    private _successMessage: Optional<string> = null;
    private _errorMessage: Optional<string> = null;

    constructor(
        private appUserService: AppUserService, 
        private friendshipService: FriendshipService, 
        private router: Router, 
        private appState: AppState) {}

    attached() {}

    activate(params: any) {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        }
    }

    onSubmit(event: Event) {
        event.preventDefault();
        this.getSearchResults(this._searchInput);
    }

    addFriend(event: Event, friendId: string) {
        event.preventDefault();
        this.sendFriendRequest(friendId);
    }

    private sendFriendRequest(friendId: string) {
        this.friendshipService
        .createPending(friendId)
        .then((response) => {
            if(Utils.isSuccessful(response)) {
                this._isFriend = true;
                this._successMessage = this.MESSAGE_FRIEND_REQUEST_SENT;
            } else {
                this._isFriend = false;
                this._errorMessage = Utils.getErrorMessage(response);
            }
        })
        .catch((error) => {
            console.log(error);
        });
    }

    private getSearchResults(inputValue: string): void {
        if(inputValue.length && inputValue !== '*') {
            let validatedInputValue = this.validateUserInput(inputValue);
            this.getAppUser(validatedInputValue);
        } else {
            this.getAppUsers();
        }
    }

    private getAppUsers(): void {
        this.appUserService
            .getAllUsers()
            .then((response) => {
                if(Utils.isSuccessful(response) && response.data) {
                    this._users = response.data;
                    this._errorMessage = null;

                    this._users.forEach(user => {
                        user.lastActive = Utils.formatAsHtml5Date(user.lastActive);
                    });
                } else {
                    this._users = [];
                    this.handleErrors(response);
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }

    private getAppUser(inputValue: string): void {
        this.appUserService
            .getUsersByName(inputValue)
            .then((response) => {
                if(Utils.isSuccessful(response) && response.data) {
                    this._users = response.data;
                    this._errorMessage = null;

                    this._users.forEach(user => {
                        user.lastActive = Utils.formatAsHtml5Date(user.lastActive);
                    });
                } else {
                    this._users = [];
                    this.handleErrors(response);
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }

    private validateUserInput(inputValue: string) {
        return inputValue; // TODO
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
                this._errorMessage = `Could not find user with name ${this._searchInput}`;
                break;
            default:
                this._errorMessage = Utils.getErrorMessage(response);
        }
    }
}

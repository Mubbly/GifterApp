import { autoinject } from "aurelia-framework";
import { Router } from "aurelia-router";
import { FriendshipService } from "service/friendshipService";
import * as UtilFunctions from "utils/utilFunctions";
import { IStatus } from "domain/IStatus";
import { IActionType } from "domain/IActionType";
import { IFriendshipCreate } from '../../domain/IFriendship';
import { Optional } from "types/generalTypes";
import { IFetchResponse } from "types/IFetchResponse";
import { ActionTypes, Statuses } from "domain/PredefinedData";
import { AppState } from "state/appState";
import * as Utils from 'utils/utilFunctions';

@autoinject
export class FriendshipsCreate {
    private _friendUserId: string = '';
    private _friendUserName: string = '';
    private _confirmationText: string = '';
    private _errorMessage: Optional<string> = null;

    constructor(
        private friendshipService: FriendshipService,
        private router: Router,
        private appState: AppState
    ) {}

    attached() {
    }

    activate(params: any) {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        } else {
            this._friendUserId = params.id;
            this._friendUserName = this._friendUserName;

           this._confirmationText = `Do you want to send a friend request to ${this._friendUserName}?`
        }
    }

    private createFriendship(newFriendship: IFriendshipCreate): void {
        this.friendshipService
            .create(newFriendship)
            .then((response: IFetchResponse<IFriendshipCreate>) => {
                if (UtilFunctions.isSuccessful(response)) {
                    this.router.navigateToRoute('profilesPersonal', {});
                } else {
                    this._errorMessage = UtilFunctions.getErrorMessage(response);
                }
            })
            .catch((error) => {
                console.log(error);
            });
    }
}

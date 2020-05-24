import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction, Router } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IFriendship } from 'domain/IFriendship';
import { FriendshipService } from 'service/friendshipService';
import * as UtilFunctions from 'utils/utilFunctions';
import { AppState } from 'state/appState';
import * as Utils from 'utils/utilFunctions';

@autoinject
export class FriendshipDetails {
    private _friendship: Optional<IFriendship> = null;
    private _errorMessage: Optional<string> = null;

    constructor(private friendshipService: FriendshipService, private appState: AppState, private router: Router) {}

    attached() {}

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
        if(!this.appState.jwt) {
            this.router.navigateToRoute(Utils.LOGIN_ROUTE);
        }
        this.getFriendship(params.id);
    }

    private getFriendship(id: string): void {
        if(UtilFunctions.existsAndIsString(id)) {
            this.friendshipService.get(id).then(
                response => {
                    if(UtilFunctions.isSuccessful(response)) {
                        this._friendship = response.data!;
                    } else {
                        this._errorMessage = UtilFunctions.getErrorMessage(response);

                    }
                }
            )
        }
    }
}

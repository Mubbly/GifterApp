import { autoinject } from 'aurelia-framework';
import { RouteConfig, NavigationInstruction } from 'aurelia-router';
import { Optional } from 'types/generalTypes';
import { IFriendship } from 'domain/IFriendship';
import { FriendshipService } from 'service/friendshipService';
import * as UtilFunctions from 'utils/utilFunctions';

@autoinject
export class FriendshipDetails {
    private _friendship: Optional<IFriendship> = null;
    private _errorMessage: Optional<string> = null;

    constructor(private friendshipService: FriendshipService) {}

    attached() {}

    activate(params: any, routeConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
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
